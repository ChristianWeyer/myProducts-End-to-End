/// <vs BeforeBuild='html2js' />
module.exports = function (grunt) {
    "use strict";

    grunt.initConfig({
        html2js: {
            options: {
                base: "client/app",
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
                src: ["client/app*/**/*.html"],
                dest: "client/app/embeddedTemplates.js"
            },
        }
    });

    grunt.loadNpmTasks("grunt-html2js");
};