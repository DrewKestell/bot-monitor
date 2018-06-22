/// <binding Clean='minify:css' ProjectOpened='watch' />
"use strict";

var gulp = require('gulp'),
    compileSass = require('gulp-sass'),
    cleanCSS = require('gulp-clean-css'),
    rename = require('gulp-rename'),
    sourcemaps = require('gulp-sourcemaps');

var webRoot = './wwwroot/';

gulp.task('compile:sass', function () {
    return gulp.src('./Styles/styles.scss')
        .pipe(compileSass())
        .pipe(gulp.dest(webRoot + 'css'));
});

gulp.task('minify:css', ['compile:sass'], function () {
    gulp.src([webRoot + 'css/*.css', '!' + webRoot + 'css/*.min.css'])
        .pipe(sourcemaps.init())
        .pipe(cleanCSS({ compatibility: '*' }))
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(webRoot + 'css'));
});

gulp.task('watch', function () {
    gulp.watch('./Styles/styles.scss', ['minify:css']);
});
