var gulp = require('gulp'),
    minifyHTML = require('gulp-minify-html'),
    ngHtml2Js = require("gulp-ng-html2js"),
    concat = require('gulp-concat');

gulp.task('html2js', function() {
    gulp.src("./client/app/**/*.html")
        .pipe(minifyHTML())
        .pipe(ngHtml2Js({
            moduleName: "myApp.embeddedTemplates" }))
        .pipe(concat("embeddedTemplates.js"))
        .pipe(gulp.dest("./client/app"));
});

gulp.task('watch-view-templates', function() {
    gulp.watch("**/*.html", { cwd: './client/app' }, ['html2js']);
});
