var tsjsmapjsSuffix = ".{ts,js.map,js}";

var appFolder = "Client/App/";

var config = {
    base: "Client",
    bowerBase: "bower_components",
    buildDir: "./wwwroot/",
    debug: "debug",
    release: "release",
    css: "css",

    bootFile: appFolder + "index.html",

    images: [appFolder + "Images/**/*.{gif,jpg,png}"],
    imageBase: "Client/App/Images",
    imageDest: "img/",

    // The scripts we want Gulp to process
    scripts : [
        // Bootstrapping
        appFolder + "app" + tsjsmapjsSuffix,
        appFolder + "config.route" + tsjsmapjsSuffix,

        // features
        appFolder + "Services/**/*" + tsjsmapjsSuffix,
        appFolder + "Main/**/*" + tsjsmapjsSuffix,
        appFolder + "Dashboard/**/*" + tsjsmapjsSuffix
    ],

    // The styles we want Gulp to process
    styles : [
        appFolder + "main.scss"
    ],

    wiredepOptions : {
        ignorePath: ".."
    },

};

config.debugFolder = config.buildDir + config.debug + "/";
config.releaseFolder = config.buildDir + config.release + "/";

config.templateFiles = [
    appFolder + "**/*.html",
    "!" + config.bootFile // Exclude the launch page
];

module.exports = config;