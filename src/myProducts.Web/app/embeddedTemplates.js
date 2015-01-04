angular.module('templates-main', ['articleDetails/articleDetails.html', 'articles/articles.html', 'easteregg/easteregg.html', 'easteregg/maze.html', 'gallery/gallery.html', 'info/info.html', 'log/log.html', 'login/login.html', 'navigation/navigation.html', 'settings/settings.html', 'start/start.html', 'statistics/statistics.html', 'statusBar/statusBar.html']);

angular.module("articleDetails/articleDetails.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("articleDetails/articleDetails.html",
    "<h3 translate=\"DETAILS_TITLE\"></h3>\n" +
    "<form name=\"form\" novalidate class=\"form-horizontal\">\n" +
    "    <div class=\"form-group\" ng-class=\"{ error: form.name.$invalid }\">\n" +
    "        <label class=\"control-label col-md-1\" for=\"name\" translate=\"DETAILS_NAME\" style=\"font-weight:normal\"></label>\n" +
    "        <div class=\"col-md-4\">\n" +
    "            <input type=\"text\" required ng-model=\"articledetails.article.Name\" name=\"name\" server-validate=\"data.Name\" class=\"form-control\">\n" +
    "        </div>\n" +
    "        <span class=\"col-md-3 help-block alert alert-danger\" ng-repeat=\"errorMessage in form.name.$error.server\" style=\"padding:10px;margin:0\">{{ errorMessage }}</span>\n" +
    "    </div>\n" +
    "    <div class=\"form-group\" ng-class=\"{ error: form.code.$invalid }\">\n" +
    "        <label class=\"control-label col-md-1\" for=\"code\" translate=\"DETAILS_CODE\" style=\"font-weight:normal\"></label>\n" +
    "        <div class=\"col-md-4\">\n" +
    "            <input type=\"text\" required ng-model=\"articledetails.article.Code\" name=\"code\" class=\"form-control\">\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"form-group\">\n" +
    "        <label class=\"control-label col-md-1\" for=\"categories\" translate=\"DETAILS_CATEGORIES\" style=\"font-weight:normal\"></label>\n" +
    "        <div class=\"col-md-2\">\n" +
    "            <select name=\"categories\" ng-model=\"articledetails.article.Category\" ng-options=\"c as c.Name for c in articledetails.categories track by c.Id\" class=\"form-control\"></select>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"form-group\">\n" +
    "        <div class=\"col-md-4\">\n" +
    "            <textarea type=\"text\" class=\"form-control\" rows=\"4\" ng-model=\"articledetails.article.Description\"></textarea>\n" +
    "        </div>\n" +
    "        <div class=\"col-md-6\">\n" +
    "            <img ng-if=\"articledetails.editMode\" alt=\"Product image\" class=\"img-responsive\" src=\"../assets/images/default.png\" ng-src=\"{{ articledetails.article.ImageUrl | baseUrl }}\" />\n" +
    "        </div>\n" +
    "        <span ng-if=\"!articledetails.editMode\">\n" +
    "            <img ng-if=\"articledetails.image\" ng-src=\"{{ articledetails.image.resized.dataURL }}\" type=\"{{ articledetails.image.file.type }}\" />\n" +
    "            <br />\n" +
    "            <br />\n" +
    "            <input type=\"file\" accept=\"image/*\" image=\"articledetails.image\" resize-max-width=\"400\" resize-quality=\"0.9\" button-text=\"{{ 'DETAILS_SELECTIMAGE' | translate }}\" />\n" +
    "        </span>\n" +
    "    </div>\n" +
    "    <div class=\"form-actions\">\n" +
    "        <button ng-click=\"articledetails.save()\" class=\"btn btn-primary\" translate=\"COMMON_SAVE\"></button>\n" +
    "        <a href=\"#/articles\" class=\"btn btn-default\" translate=\"COMMON_CANCEL\"></a>\n" +
    "    </div>\n" +
    "</form>\n" +
    "<br />\n" +
    "<br />\n" +
    "");
}]);

angular.module("articles/articles.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("articles/articles.html",
    "<h3 translate=\"OVERVIEW_TITLE\"></h3>\n" +
    "\n" +
    "<div>\n" +
    "    <div>\n" +
    "        <form id=\"searchTextForm\" class=\"form-inline\">\n" +
    "            <div class=\"form-group\">\n" +
    "                <label for=\"searchtext\" translate=\"OVERVIEW_FILTER\" style=\"font-weight:normal\"></label>\n" +
    "                <input type=\"text\" id=\"searchtext\" class=\"form-control input-sm\" ng-model=\"articles.selectedFilteredArticle\" typeahead=\"article.Name for article in articles.getFilteredData($viewValue)\">\n" +
    "                <!--<i class=\"fa fa-refresh\" ng-click=\"articles.getPagedData(true)\"></i>-->\n" +
    "            </div>\n" +
    "        </form>\n" +
    "    </div>\n" +
    "    <div class=\"hidden-xs\">\n" +
    "        <br/>\n" +
    "    </div>\n" +
    "    <div ng-swipe-right=\"swipeRight()\" ng-swipe-left=\"swipeLeft()\">\n" +
    "        <table ng-if=\"articles.articlesData\" class=\"table table-striped table-hover\">\n" +
    "            <thead>\n" +
    "                <tr>\n" +
    "                    <th>Name</th>\n" +
    "                    <th class =\"hidden-xs\">Code</th>\n" +
    "                    <th style=\"width: 90px\">\n" +
    "                        <i ng-if=\"capabilities.has('AddArticle')\" class=\"btn btn-primary add-btn-primary glyphicon glyphicon-plus\" ng-click=\"articles.addArticle()\"></i>\n" +
    "                    </th>\n" +
    "                </tr>\n" +
    "            </thead>\n" +
    "            <tr ng-repeat=\"a in articles.articlesData track by $index\">\n" +
    "                <td ng-click=\"articles.getArticleDetails(a.Id)\">{{:: a.Name }}</td>\n" +
    "                <td class=\"hidden-xs\" ng-click=\"articles.getArticleDetails(a.Id)\">{{:: a.Code }}</td>\n" +
    "                <td style=\"width: 90px\">\n" +
    "                    <i class=\"btn btn-success list-btn-success glyphicon glyphicon-edit\" ng-click=\"articles.getArticleDetails(a.Id)\"></i>\n" +
    "                    <i class=\"btn btn-danger list-btn-danger glyphicon glyphicon-trash\" ng-click=\"articles.deleteArticle(a.Id)\"></i>\n" +
    "                </td>\n" +
    "            </tr>\n" +
    "        </table>\n" +
    "    </div>\n" +
    "    <div class=\"text-center\">\n" +
    "        <pagination ng-if=\"articles.articlesData\" ng-model=\"articles.pagingOptions.currentPage\" boundary-links=\"true\" total-items=\"articles.totalServerItems\" max-size=\"3\" rotate=\"false\" items-per-page=\"articles.pagingOptions.pageSize\" page=\"articles.pagingOptions.currentPage\" previous-text=\"&lsaquo;\" next-text=\"&rsaquo;\" first-text=\"&laquo;\" last-text=\"&raquo;\"></pagination>\n" +
    "    </div>\n" +
    "</div>\n" +
    "\n" +
    "<br />\n" +
    "<br />\n" +
    "");
}]);

angular.module("easteregg/easteregg.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("easteregg/easteregg.html",
    "<easteregg-maze></easteregg-maze>");
}]);

angular.module("easteregg/maze.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("easteregg/maze.html",
    "<div class=\"three\">\n" +
    "    <button class=\"btn btn-default generate\">\n" +
    "        Re-Generate maze\n" +
    "    </button>\n" +
    "    <span class=\"pull-right\">Credits: http://www.johansatge.fr/three-maze/</span>\n" +
    "</div>\n" +
    "");
}]);

angular.module("gallery/gallery.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("gallery/gallery.html",
    "<h3 translate=\"GALLERY_TITLE\"></h3>\n" +
    "\n" +
    "<span translate=\"GALLERY_BODY\"></span>\n" +
    "\n" +
    "<br />\n" +
    "<br />\n" +
    "<br />\n" +
    "<div>\n" +
    "    <ul rn-carousel rn-carousel-indicator class=\"carouselImages\">\n" +
    "        <li ng-repeat=\"image in gallery.productImages track by $index\">\n" +
    "            <img ng-src=\"{{:: image | baseUrl  }}\" class=\"carouselImage\" />\n" +
    "        </li>\n" +
    "    </ul>\n" +
    "</div>\n" +
    "");
}]);

angular.module("info/info.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("info/info.html",
    "<h3 translate=\"ABOUT_TITLE\"></h3>\n" +
    "\n" +
    "<span translate=\"ABOUT_BODY\"></span>\n" +
    "\n" +
    "<br />\n" +
    "<br />\n" +
    "<div>\n" +
    "    Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.\n" +
    "    <br />\n" +
    "    At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.\n" +
    "</div>\n" +
    "\n" +
    "<br />\n" +
    "<!--<iframe width=\"100%\" height=\"600\" src=\"https://www.thinktecture.com/contact\"></iframe>-->\n" +
    "\n" +
    "<br />\n" +
    "<div ng-click=\"events.clickMe()\">\n" +
    "    {{:: data.message }}\n" +
    "</div>\n" +
    "<br />\n" +
    "<span translate=\"ABOUT_CONTACT\"></span>\n" +
    "");
}]);

angular.module("log/log.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("log/log.html",
    "<h3 translate=\"LOGS_TITLE\"></h3>\n" +
    "\n" +
    "<span>Live:</span>\n" +
    "<br/>\n" +
    "\n" +
    "<div>\n" +
    "    <table class=\"table table-striped table-hover table-condensed\">\n" +
    "        <thead>\n" +
    "            <tr>\n" +
    "                <th></th>\n" +
    "            </tr>\n" +
    "        </thead>\n" +
    "        <tbody>\n" +
    "            <tr ng-repeat=\"logEntry in log.entries track by $index\">\n" +
    "                <td>{{:: logEntry.RenderedMessage }}</td>\n" +
    "            </tr>\n" +
    "        </tbody>\n" +
    "    </table>\n" +
    "</div>\n" +
    "");
}]);

angular.module("login/login.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("login/login.html",
    "<div class=\"container\">\n" +
    "    <div class=\"login-container\">\n" +
    "        <div class=\"avatar\"></div>\n" +
    "        <div class=\"form-box\">\n" +
    "            <form name=\"form\" novalidate>\n" +
    "                <input name=\"user\" type=\"text\" placeholder=\"{{ 'LOGIN_USERNAME' | translate }}\" required ng-model=\"login.username\">\n" +
    "                <input type=\"password\" placeholder=\"{{ 'LOGIN_PASSWORD' | translate }}\" required ng-model=\"login.password\">\n" +
    "                <button class=\"btn btn-success btn-block login\" ng-click=\"login.submit()\" ng-disabled=\"form.$invalid\" translate=\"LOGIN_BUTTON_LOGIN\"></button>\n" +
    "            </form>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("navigation/navigation.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("navigation/navigation.html",
    "<div class=\"navbar navbar-default navbar-fixed-top\" style=\"position:fixed\" bs-navbar ng-cloak translate-cloak>\n" +
    "    <div class=\"container-fluid\">\n" +
    "        <div class=\"navbar-header\">\n" +
    "            <button type=\"button\" class=\"navbar-toggle\" ng-click=\"navigation.isCollapsed=!navigation.isCollapsed\">\n" +
    "                <span class=\"icon-bar\"></span><span class=\"icon-bar\"></span><span class=\"icon-bar\"></span>\n" +
    "            </button>\n" +
    "            <span class=\"navbar-brand\">\n" +
    "                <img src=\"../assets/images/logo.png\" width=\"74px\" height=\"17px\" /><br />\n" +
    "                <span style=\"font-size: 0.75em\"><a href=\"#/\" data-match-route=\"/#\">myProducts</a></span>\n" +
    "            </span>\n" +
    "        </div>\n" +
    "        <div class=\"navbar-collapse\" collapse=\"navigation.isCollapsed\">\n" +
    "            <ul class=\"nav navbar-nav\">\n" +
    "                <li ng-repeat=\"ni in navigation.navigationItems track by $index\" data-match-route='{{ ni.MatchPattern }}'>\n" +
    "                    <a ng-show=\"ni.DisplayText\" ng-click=\"navigation.isCollapsed=true\" ng-href=\"#{{ ni.Url }}\" translate>{{ ni.DisplayText }}</a>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "\n" +
    "            <ul class=\"nav navbar-nav navbar-right\">\n" +
    "                <li data-match-route=\"/settings\"><a ng-click=\"navigation.isCollapsed=true\" href=\"#/settings\" translate=\"INDEX_SETTINGS\"></a></li>\n" +
    "\n" +
    "                <li class=\"dropdown\" dropdown>\n" +
    "                    <a class=\"dropdown-toggle\" dropdown-toggle>\n" +
    "                        <i class=\"glyphicon glyphicon-align-justify\"></i>&nbsp;\n" +
    "                        <span translate=\"COMMON_LANG\"></span>\n" +
    "                    </a>\n" +
    "                    <ul class=\"dropdown-menu\">\n" +
    "                        <li>\n" +
    "                            <a ng-click=\"navigation.isCollapsed=true; navigation.changeLanguage('de');\">\n" +
    "                                <img src=\"../assets/images/lang_de_t.png\" width=\"36px\" height=\"26px\">\n" +
    "                                <span translate=\"COMMON_DE\"></span>\n" +
    "                            </a>\n" +
    "                        </li>\n" +
    "                        <li>\n" +
    "                            <a ng-click=\"navigation.isCollapsed=true; navigation.changeLanguage('en')\">\n" +
    "                                <img src=\"../assets/images/lang_en_t.png\" width=\"36px\" height=\"26px\">\n" +
    "                                <span translate=\"COMMON_EN\"></span>\n" +
    "                            </a>\n" +
    "                        </li>\n" +
    "                    </ul>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>");
}]);

angular.module("settings/settings.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("settings/settings.html",
    "<h3 translate=\"SETTINGS_TITLE\"></h3>\n" +
    "<div>\n" +
    "    <form class=\"form-horizontal\">\n" +
    "        <div class=\"form-group\">\n" +
    "            <label for=\"pushCheckbox\" class=\"col-md-2 control-label\" translate=\"SETTINGS_PUSH\"></label>\n" +
    "            <div class=\"col-md-10\">\n" +
    "                <input type=\"checkbox\" class=\"form-control\" id=\"pushCheckbox\" bs-switch switch-on-label=\"{{ 'SETTINGS_YES' | translate }}\" switch-off-label=\"{{ 'SETTINGS_NO' | translate }}\" ng-model=\"settings.enablePush\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </form>\n" +
    "</div>\n" +
    "");
}]);

angular.module("start/start.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("start/start.html",
    "<div class=\"metrouicss\" style=\"padding-top: 1em\" ng-cloak>\n" +
    "    <a ng-repeat=\"ni in start.navigationItems\" ng-href=\"#{{ ni.Url }}\">\n" +
    "        <div class=\"tile\" ng-class=\"{{ start.classes[$index % start.classes.length] }}\">\n" +
    "            <div class=\"brand\">\n" +
    "                <div class=\"name\" translate>{{:: ni.DisplayText }}</div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </a>\n" +
    "    <a href=\"#/info\">\n" +
    "        <div class=\"tile bg-color-lighten\">\n" +
    "            <div class=\"brand\">\n" +
    "                <div class=\"name\" style=\"color: black\">Info</div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </a>\n" +
    "</div>\n" +
    "");
}]);

angular.module("statistics/statistics.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("statistics/statistics.html",
    "<h3 translate=\"STATS_TITLE\"></h3>\n" +
    "\n" +
    "<br />\n" +
    "<div class=\"row\">\n" +
    "    <div class=\"col-md-6\">\n" +
    "        <div class=\"panel panel-default\">\n" +
    "            <div class=\"panel-heading\">Sales</div>\n" +
    "            <div class=\"panel-body\">\n" +
    "                <canvas id=\"doughnut\" class=\"chart chart-doughnut\"\n" +
    "                        data=\"statistics.pieData\"\n" +
    "                        labels=\"statistics.pieLabels\"\n" +
    "                        legend=\"true\"></canvas>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"col-md-6\">\n" +
    "        <div class=\"panel panel-default\">\n" +
    "            <div class=\"panel-heading\">Series</div>\n" +
    "            <div class=\"panel-body\">\n" +
    "                <canvas id=\"bar\" class=\"chart chart-bar\"\n" +
    "                        data=\"statistics.columnData\"\n" +
    "                        series=\"statistics.columnSeries\"\n" +
    "                        labels=\"statistics.columnLabels\"\n" +
    "                        legend=\"true\"></canvas>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>");
}]);

angular.module("statusBar/statusBar.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("statusBar/statusBar.html",
    "<footer id=\"footer\" ng-cloak translate-cloak>\n" +
    "    <div class=\"container-fluid\">\n" +
    "        <div ng-if=\"!status.isOnline\" class=\"circle-red pull-left\"></div>\n" +
    "        <div ng-if=\"status.isOnline\" class=\"circle-green pull-left\"></div>\n" +
    "\n" +
    "        <span class=\"pull-right\">\n" +
    "            <span ng-if=\"!tt.authentication.userLoggedIn\" data-match-route=\"/login\">\n" +
    "                <a href=\"#/login\" translate=\"INDEX_LOGIN\"></a>\n" +
    "            </span>\n" +
    "            <span ng-if=\"tt.authentication.userLoggedIn\">\n" +
    "                <a href=\"\" ng-click=\"navigation.logout()\"><i class=\"fa fa-sign-out\" style=\"color: white\"></i></a>\n" +
    "            </span>\n" +
    "        </span>\n" +
    "    </div>\n" +
    "</footer>");
}]);
