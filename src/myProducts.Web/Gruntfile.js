module.exports = function (grunt) {
    "use strict";

    grunt.initConfig({
        html2js: {
            options: {
                base: "app",
                module: "myApp.embeddedTemplates",
                singleModule: true,
                useStrict: true,
                htmlmin: {
                    collapseBooleanAttributes: true,
                    collapseWhitespace: true,
                    removeAttributeQuotes: true,
                    removeComments: true,
                    removeEmptyAttributes: true,
                    removeRedundantAttributes: true,
                    removeScriptTypeAttributes: true,
                    removeStyleLinkTypeAttributes: true
                }
            },
            main: {
                src: ["app*/**/*.html"],
                dest: "app/embeddedTemplates.js"
            },
        }
    });

    grunt.loadNpmTasks("grunt-html2js");
};