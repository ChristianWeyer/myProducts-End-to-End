var fs = require('fs')
    , _ = require('underscore')._;

var _bundles = {};

exports.renderStyles = function (name) {
    var bundle = _bundles[name];
    var response = '\n';
    
    if (bundle) {
        bundle.forEach(function (path) {
            response += '    <link href="' + path + '" rel="stylesheet" />\n';
        });
    }

    return response;
};

exports.renderScripts = function (name) {
    var bundle = _bundles[name];
    var response = '\n';
    if (bundle) {
        bundle.forEach(function (path) {
            response += '<script src="' + path + '" type="text/javascript"></script>\n';
        });
    }

    return response;
};

exports.addBundle = function (name, filetype, directoryConfigurations) {
    var filesInBundle = [];

    directoryConfigurations.forEach(function (item) {
        addFiles(item.baseDirectory, item.path, filetype, item.includeSubdirectories, filesInBundle);
    });

    _bundles[name] = filesInBundle;
};

exports.addBundleFromFiles = function (name, filesInBundle) {
    _bundles[name] = filesInBundle;
};

var addFiles = function (baseDirectory, path, filetype, includeSubdirectories, filesInBundle) {
    var subDirs = [];

console.log("###" + baseDirectory + path);

    var files = fs.readdirSync(baseDirectory + path);

    files = _.sortBy(files, function (p) {
        return p
    });

    filetype = filetype.toUpperCase();

    files.forEach(function (file) {
        var fullPath = baseDirectory + path + '/' + file;
        var stat = fs.statSync(fullPath);

        if (stat.isFile()) {
            if (file.length > filetype.length + 2) {
                var ext = file.substr(file.length - filetype.length - 1, filetype.length + 1);
                if (ext.toUpperCase() == '.' + filetype) {
                    filesInBundle.push(path + '/' + file);
                }
            }
        }

        if (includeSubdirectories && stat.isDirectory()) {
            subDirs.push(file);
        }
    });

    subDirs.forEach(function (dir) {
        addFiles(baseDirectory, path + '/' + dir, filetype, includeSubdirectories, filesInBundle);
    })
};