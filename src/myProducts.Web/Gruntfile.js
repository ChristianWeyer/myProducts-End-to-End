module.exports = function (grunt) {
    "use strict";

    grunt.initConfig({
        html2js: {
            options: {
                base: "app"
                // custom options
            },
            main: {
                src: ["app/**/*.html"],
                dest: "app/embeddedTemplates.js"
            },
        }
    });

    grunt.loadNpmTasks("grunt-html2js");
};