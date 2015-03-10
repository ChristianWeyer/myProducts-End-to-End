var gulp = require("gulp"),
    minifyHTML = require("gulp-minify-html"),
    ngHtml2Js = require("gulp-ng-html2js"),
    concat = require("gulp-concat");

var config = {
    appFilesPath: "./client/app"
};

gulp.task("html2js", function() {
    gulp.src(config.appFilesPath + "/**/*.html")
        .pipe(minifyHTML())
        .pipe(ngHtml2Js({ moduleName: "myApp.embeddedTemplates" }))
        .pipe(concat("embeddedTemplates.js"))
        .pipe(gulp.dest(config.appFilesPath));
});

gulp.task("watch-view-templates", function() {
    gulp.watch("**/*.html", { cwd: config.appFilesPath }, ["html2js"]);
});

gulp.task("default", ["watch-view-templates"]);