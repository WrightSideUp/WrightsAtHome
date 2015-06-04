var gulp = require("gulp");

// Include Our Plugins
var concat = require("gulp-concat");
var ignore = require("gulp-ignore");
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');
var minifyCss = require("gulp-minify-css");
var uglify = require("gulp-uglify");
var rev = require("gulp-rev");
var del = require("del");
var path = require("path");
var templateCache = require("gulp-angular-templatecache");
var eventStream = require("event-stream");
var order = require("gulp-order");
var gulpUtil = require("gulp-util");
var wiredep = require("wiredep");
var inject = require("gulp-inject");
var yargs = require("yargs").argv;

// Get our config
var config = require("./gulpfile.config.js");

// Determine whether we are in debug mode by the value passed to gulp by Visual Studio in the csproj
var isDebug = !(yargs.mode === "Release");
gulpUtil.log(gulpUtil.colors.green("isDebug: " + isDebug));

// Determine whether we are in debug mode by the value passed to gulp by Visual Studio in the csproj
gulp.task("default", [(isDebug ? "build-debug" : "build-release")]);

gulp.task("build-debug", ["clean", "copyFiles", "styles", "scripts", "bootFiles"]);

gulp.task("build-release", ["clean", "copyFiles", "styles", "scripts", "bootFiles"]);


gulp.task("clean", function(cb) {
    return del([config.buildDir], cb);
});


gulp.task("copyFiles", ["clean"], function() {

    var images = gulp.src(config.images, { base: config.imageBase })
                     .pipe(gulp.dest(config.buildDir + config.imageDest));

    var scriptsAndStyles;

    if (isDebug) {

        // Copy App Scripts
        var appScripts = gulp.src(config.scripts, { base: config.base })
            .pipe(gulp.dest(config.debugFolder));

        var bowerFiles = [].concat(getBowerScripts(), getBowerStyles());

        // Copy Bower Component Scripts
        var bowerStream = gulp.src(bowerFiles, { base: config.bowerBase })
            .pipe(gulp.dest(config.debugFolder + config.bowerBase));

        scriptsAndStyles = eventStream.merge(appScripts, bowerStream);

    } else {

        scriptsAndStyles = gUtil.noop();
    }

    return eventStream.merge(images, scriptsAndStyles);

});

gulp.task("styles", ["clean"], function() {

    if (isDebug) {

        return gulp.src(config.styles)
            .pipe(sourcemaps.init())
            .pipe(sass())
            .pipe(sourcemaps.write())
            .pipe(gulp.dest(config.debugFolder));

    } else {

        var bowerCss = gulp.src(getBowerScriptsOrStyles("css"));
        var appCss = gulp.src(config.styles).pipe(sass());

        return eventStream.merge(bowerCss, appCss)
            .pipe(concat("app.css")) // Make a single file
            .pipe(minifyCss()) // Make the file titchy tiny small
            .pipe(rev()) // Suffix a version number to it
            .pipe(gulp.dest(config.releaseFolder + "/" + config.css)); // Write single versioned file to build/release folder
    }
});

gulp.task("scripts", ["clean"], function() {

    var allScripts = isDebug ?
                     [] :  // Nothing to do for Debug -- scripts copied in copyFiles
                     [].concat(getBowerScripts(), config.scripts);

    return gulp.src(allScripts)
        .pipe(ignore.exclude("**/*.{ts,js.map}")) // Exclude ts and js.map files - not needed in release mode
        .pipe(concat("app.js")) // Make a single file                                                         
        .pipe(uglify()) // Make the file titchy tiny small
        .pipe(rev()) // Suffix a version number to it
        .pipe(gulp.dest(config.releaseFolder)); // Write single versioned file to build/release folder
});

gulp.task("bootFiles", ["clean", "copyFiles", "scripts", "styles"], function() {

    var scriptsAndStyles = [].concat(getBowerScripts(), config.scripts, getBowerStyles());

    if (isDebug) {

        return gulp
            .src(config.bootFile)
            .pipe(inject(
                gulp.src(config.debugFolder + "**/*.{js,css}", { read: false })
                .pipe(order(scriptsAndStyles))
            ))
            .pipe(gulp.dest(config.buildDir));
    } else {

        return gulp
            .src(config.bootFile)
            .pipe(inject(gulp.src(config.releaseFolder + "**/*.{js,css}", { read: false }), { removeTags: true }))
            .pipe(gulp.dest(config.buildDir));
    }
});

/**
 * Get the scripts or styles from bower the app requires
 * 
 * @param {string} jsOrCss Should be "js" or "css"
 */
function getBowerScripts() {

    return wiredep(config.wiredepOptions)["js"].map(function(file) {
        return path.relative('', file);
    });
}

function getBowerStyles() {
    return wiredep(config.wiredepOptions)["css"].map(function (file) {
        return path.relative('', file);
    });
}


