var express = require('express'),
    http = require('http'),
    walrus = require('walrus'),
    consolidate = require('consolidate'),
    config = require('./node/config.js'),
    bundler = require("./node/bundler.js"),
    browserSync = require('browser-sync');

var app = express();
var appDir = __dirname + "/client";
var virtualDir = "/";

var args = process.argv.slice(2);
var port = process.env.PORT || args[0] || 8090;

app.configure(function () {
    app.engine("walrus", consolidate.walrus);
    app.set('port', port);
    app.set('views', appDir);
    app.set('view engine', 'walrus');
    app.use(express.favicon());
    app.use(express.json());
    app.use(express.urlencoded());
    app.use(app.router);
    app.use(virtualDir, express.static(appDir));
    app.use(express.errorHandler());
});

config.initializeBundles(appDir);

app.get(virtualDir, function (req, res) {
    res.render('index', { bundler: bundler });
});

http.createServer(app).listen(app.get('port'), function () {
    console.log("node.js Express server - listening on port " + app.get('port'));

    browserSync({
        proxy: 'localhost:' + port,
        files: ['client/**/*.{html, js, css}']
    });
});
