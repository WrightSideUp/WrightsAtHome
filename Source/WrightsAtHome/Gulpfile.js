var gulp = require('gulp');
var bower = require('gulp-bower');
var concat = require('gulp-concat');
var karma = required('gulp-karma');

gulp.task('bower', function() {
    return bower('./bower_components');
});

gulp.task('copyLibs', ['bower'], function() {
    gulp.src(['bower_components/angular/*.*'])
        .pipe(gulp.dest('public/libs/angular'));


})
