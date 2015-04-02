(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('articleDetails/articleDetails.html',
    '<h3 translate=DETAILS_TITLE></h3><form name=form novalidate class=form-horizontal><div class=form-group ng-class="{ error: form.name.$invalid }"><label class="control-label col-md-1" for=name translate=DETAILS_NAME style=font-weight:normal></label><div class=col-md-4><input type=text required ng-model=articledetails.article.Name name=name server-validate=data.Name class=form-control></div><span class="col-md-3 help-block alert alert-warning" ng-repeat="errorMessage in form.name.$error.server" style=padding:10px;margin:0>{{ errorMessage }}</span></div><div class=form-group ng-class="{ error: form.code.$invalid }"><label class="control-label col-md-1" for=code translate=DETAILS_CODE style=font-weight:normal></label><div class=col-md-4><input type=text required ng-model=articledetails.article.Code name=code class=form-control></div></div><div class=form-group><label class="control-label col-md-1" for=categories translate=DETAILS_CATEGORIES style=font-weight:normal></label><div class=col-md-2><select name=categories ng-model=articledetails.article.Category ng-options="c.Name for c in articledetails.categories track by c.Id" class=form-control></select></div></div><div class=form-group><div class=col-md-4><textarea type=text class=form-control rows=4 ng-model=articledetails.article.Description></textarea></div><div class=col-md-6><img ng-if=articledetails.editMode alt="Product image" class=img-responsive src=../assets/images/default.png ng-src="{{ articledetails.article.ImageUrl | baseUrl }}"></div><span ng-if=!articledetails.editMode><img ng-if=articledetails.image ng-src="{{ articledetails.image.resized.dataURL }}" type="{{ articledetails.image.file.type }}"><br><br><input type=file accept=image/* image=articledetails.image resize-max-width=400 resize-quality=0.9 button-text="{{ \'DETAILS_SELECTIMAGE\' | translate }}"></span></div><div class=form-actions><button ng-click=articledetails.save() class="btn btn-primary" translate=COMMON_SAVE></button> <a href=#/articles class="btn btn-default" translate=COMMON_CANCEL></a></div></form><br><br>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('articles/articles.html',
    '<h3 translate=OVERVIEW_TITLE></h3><div><div><form id=searchTextForm class=form-inline><div class=form-group><label class=sr-only for=searchtext translate=OVERVIEW_FILTER style=font-weight:normal></label> <input type=text placeholder="{{ \'OVERVIEW_FILTER\' | translate }}" translate-cloak="" id=searchtext class="form-control input-sm" ng-model=articles.selectedFilteredArticle typeahead="article.Name for article in articles.getFilteredData($viewValue)"></div></form></div><div class=hidden-xs><br></div><div ng-swipe-right=swipeRight() ng-swipe-left=swipeLeft()><table ng-if=articles.articlesData class="table table-striped table-hover"><thead><tr><th>Name</th><th class=hidden-xs>Code</th><th style="width: 90px"><i ng-if="capabilities.has(\'AddArticle\')" class="btn btn-primary add-btn-primary glyphicon glyphicon-plus" ng-click=articles.addArticle()></i></th></tr></thead><tr ng-repeat="a in articles.articlesData track by $index"><td ng-click=articles.getArticleDetails(a.Id)>{{ a.Name }}</td><td class=hidden-xs ng-click=articles.getArticleDetails(a.Id)>{{ a.Code }}</td><td style="width: 90px"><i class="btn btn-success list-btn-success glyphicon glyphicon-edit" ng-click=articles.getArticleDetails(a.Id)></i> <i class="btn btn-danger list-btn-danger glyphicon glyphicon-trash" ng-click=articles.deleteArticle(a.Id)></i></td></tr></table></div><div ng-if=articles.articlesData class=text-center><pagination ng-model=articles.pagingOptions.currentPage boundary-links=true total-items=articles.totalServerItems max-size=3 rotate=false items-per-page=articles.pagingOptions.pageSize page=articles.pagingOptions.currentPage previous-text=&lsaquo; next-text=&rsaquo; first-text=&laquo; last-text=&raquo;></pagination></div></div><br><br>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('easteregg/easteregg.html',
    '<easteregg-maze></easteregg-maze>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('easteregg/maze.html',
    '<div class=three><button class="btn btn-default generate">Re-Generate maze</button> <span class=pull-right>Credits: http://www.johansatge.fr/three-maze/</span></div>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('gallery/gallery.html',
    '<h3 translate=GALLERY_TITLE></h3><span translate=GALLERY_BODY></span><br><br><br><div><ul rn-carousel="" rn-carousel-index=imagesIndex class=carouselImages><li ng-repeat="image in gallery.productImages track by $index"><img ng-src="{{:: image | baseUrl }}"></li></ul><div class=carouselIndicators rn-carousel-indicators="" slides=gallery.productImages rn-carousel-index=imagesIndex></div></div>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('info/info.html',
    '<h3 translate=ABOUT_TITLE></h3><span translate=ABOUT_BODY></span><br><br><div>Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.<br>At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</div><br><br><div ng-click=events.clickMe()>{{:: data.message }}</div><br><span translate=ABOUT_CONTACT></span>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('log/log.html',
    '<h3 translate=LOGS_TITLE></h3><span>Live:</span><br><div><table class="table table-striped table-hover table-condensed"><thead><tr><th></th></tr></thead><tbody><tr ng-repeat="logEntry in log.entries track by $index"><td>{{:: logEntry.RenderedMessage }}</td></tr></tbody></table></div>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('login/login.html',
    '<div class=container><div class=login-container><div class=avatar></div><div class=form-box><form name=form novalidate><oauth site=https://localhost/ngmd/idsrv authorize-path=/connect/authorize client-id=myp-implicitclient redirect-uri="https://localhost/ngmd/client/" scope=default template=libs/60_oauth-ng/views/templates/default.html></oauth></form></div></div></div>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('navigation/navigation.html',
    '<div class="navbar navbar-default navbar-fixed-top" style=position:fixed bs-navbar="" ng-cloak=""><div class=container-fluid><div class=navbar-header><button type=button class=navbar-toggle ng-click="navigation.isCollapsed=!navigation.isCollapsed"><span class=icon-bar></span><span class=icon-bar></span><span class=icon-bar></span></button> <span class=navbar-brand><img src=assets/images/logo.png width=74px height=17px><br><span style="font-size: 0.75em"><a href="#/" data-match-route=/#>myProducts</a></span></span></div><div class=navbar-collapse collapse=navigation.isCollapsed><ul class="nav navbar-nav"><li ng-repeat="ni in navigation.navigationItems track by $index" data-match-route="{{ ni.MatchPattern }}"><a ng-show=ni.DisplayText ng-click="navigation.isCollapsed=true" ng-href="#{{ ni.Url }}" translate-cloak="">{{ ni.DisplayText | translate }}</a></li></ul><ul class="nav navbar-nav navbar-right"><li data-match-route=/settings><a ng-click="navigation.isCollapsed=true" href=#/settings translate=INDEX_SETTINGS></a></li><li class=dropdown dropdown=""><a class=dropdown-toggle dropdown-toggle=""><i class="glyphicon glyphicon-align-justify"></i>&nbsp; <span translate=COMMON_LANG></span></a><ul class=dropdown-menu><li><a ng-click="navigation.isCollapsed=true; navigation.changeLanguage(\'de\');"><img src=assets/images/lang_de_t.png width=36px height=26px> <span translate=COMMON_DE></span></a></li><li><a ng-click="navigation.isCollapsed=true; navigation.changeLanguage(\'en\')"><img src=assets/images/lang_en_t.png width=36px height=26px> <span translate=COMMON_EN></span></a></li></ul></li></ul></div></div></div>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('start/start.html',
    '<div class=metrouicss style="padding-top: 1em"><a ng-repeat="ni in start.navigationItems" ng-href="#{{ ni.Url }}"><div class=tile ng-class="{{ start.classes[$index % start.classes.length] }}" translate-cloak=""><div class=brand><div class=name>{{ ni.DisplayText | translate }}</div></div></div></a> <a href=#/info><div class="tile bg-color-lighten"><div class=brand><div class=name style="color: black">Info</div></div></div></a></div>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('statusBar/statusBar.html',
    '<footer id=footer ng-cloak=""><div class=container-fluid><div ng-if=!status.isOnline class="circle-red pull-left"></div><div ng-if=status.isOnline class="circle-green pull-left"></div><span class=pull-right><span ng-if=!tt.authentication.userLoggedIn data-match-route=/login><a href=#/login translate=INDEX_LOGIN translate-cloak=""></a></span> <span ng-if=tt.authentication.userLoggedIn><a ng-click=navigation.logout()><i class="fa fa-sign-out" style="color: white"></i></a></span></span></div></footer>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('settings/settings.html',
    '<h3 translate=SETTINGS_TITLE></h3><div><form class=form-horizontal><div class=form-group><label for=pushCheckbox class="col-md-2 control-label" translate=SETTINGS_PUSH></label><div class=col-md-10><input type=checkbox class=form-control id=pushCheckbox bs-switch="" switch-on-label="{{ \'SETTINGS_YES\' | translate }}" switch-off-label="{{ \'SETTINGS_NO\' | translate }}" ng-model=settings.enablePush></div></div></form></div>');
}]);
})();

(function(module) {
try {
  module = angular.module('myApp.embeddedTemplates');
} catch (e) {
  module = angular.module('myApp.embeddedTemplates', []);
}
module.run(['$templateCache', function($templateCache) {
  $templateCache.put('statistics/statistics.html',
    '<h3 translate=STATS_TITLE></h3><br><div class=row><div class=col-md-6><div class="panel panel-default"><div class=panel-heading>Sales</div><div class=panel-body><canvas id=doughnut class="chart chart-doughnut" data=statistics.pieData labels=statistics.pieLabels legend=true></canvas></div></div></div><div class=col-md-6><div class="panel panel-default"><div class=panel-heading>Series</div><div class=panel-body><canvas id=bar class="chart chart-bar" data=statistics.columnData series=statistics.columnSeries labels=statistics.columnLabels legend=true></canvas></div></div></div></div>');
}]);
})();
