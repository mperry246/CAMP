//=============================================================================
// jQuery extensions
//=============================================================================

var structuredIcons = __availableIcons.map(function (i) 
{
	var svg = i.indexOf("#");
	return {
		'id': i,
		'svg': svg != -1,
		'lookup':(svg ? i.substr(svg + 1) : i),
		'title': (svg ? i.substr(svg + 1) : i).replace(/'_'/i, ' ')
	}
});

for (var i = 0; i < structuredIcons.length; i++) 
{
	structuredIcons[structuredIcons[i].lookup] = structuredIcons[i];
}

function initialize()
{
	$.batchSetup(
	{
		url: 'frameset/batch',
		parse: function (data) {
			var responses = JSON.parse(data);
			if (responses) {
				$.each(responses, function (i, response) {
					if (response.body)
						response.body = JSON.parse(response.body);
				});
			}
			return responses;
		}
	});

	jQuery.event.special['click']._default = null;
	attachEventHandlers();
}

function onChildFrameUnload(e)
{
	frameHelper.startLoadAnimation();
}

//=============================================================================
// Event handlers
//=============================================================================

function attachEventHandlers()
{
	$(window).on("load resize", function ()
	{
		frameHelper.adjustNavMenu();
		frameHelper.setQuickHelpSize();
		frameHelper.adjustLeftMenu();
		// here we will adjust the links layout
		if ($("#navMenu").is(":visible"))
		{
			var searchStr = frameHelper.getSearchStr();
			if (!searchStr) frameHelper.renderModuleMenu(frameHelper.__activeModuleID);
		}

		var navBar = frameHelper.__designMode ? $("#navBarDesign") : $("#navBarMaster");
		if (navBar.length)
		{
			var resize = function () {
				var body = $("body"), pad = parseInt(body.css("padding-top"));
				if (!isNaN(pad) && pad != navBar.outerHeight())
					body.css("padding-top", navBar.outerHeight() + "px");
			};
			if (px.IsIPad || px.IsIPhone) setTimeout(function () { resize(); }, 500);
			else resize();
		}
	});

	$(window).on('popstate', function (e)
	{
		if (!frameHelper.isTopMenuMode())
		{
			var screen = px.getQueryParam("ScreenId", location.href);
			frameHelper.hideHelpFrame();
			if (screen)
			{
				var ws = frameHelper.getScreenModule(screen.split('=')[1]);
				if (ws) frameHelper.setActiveModule(ws.WorkspaceID);
			}
		}
	});

	$(window).on("navigate", function (event, data)
	{
		var direction = data.state.direction;
		if (direction == 'back')
		{
			console.log('back button was pressed')
		}
	});

	$(window).on("load", function ()
	{
		frameHelper.digitalWatch();
	});

	// ctrl-q handler
	function handleCtrlQ(e)
	{
		if (e.ctrlKey && e.keyCode === 81)
		{
			document.getElementById("searchInput").focus();
			return true;
		}
	}

	// keyboard navigation
	$(document.documentElement).on("keydown", function (e)
	{

		if (handleCtrlQ(e))
		{
			return;
		}
		var shift, moveAside, ae = document.activeElement, isTab = false;
		if (e.keyCode != 27)
		{
			if (ae && PX.formTags.indexOf(ae.tagName) >= 0) return;
			if ($("#bodyShadow").is(":visible")) return;
		}
		if ((e.altKey || e.ctrlKey) && e.keyCode != 13 && e.keyCode != 27)
				return;

		switch (e.keyCode)
		{
			case 13: 	// Enter
				var elem = frameHelper.getFocusedElement();
				if (elem) { elem.trigger("mousedown"); elem.trigger("mouseup"); elem.trigger("click"); }
				break;
			case 27: // Escape
				if (frameHelper.focusArea == 0) frameHelper.hideAllPanels();
				break;
			case 9:  // Tab
				shift = e.shiftKey ? -1 : 1; moveAside = true; isTab = true;
				break;
			case 38: // Up
				shift = -1;
				break;
			case 40: // Down
				shift = 1;
				break;
			case 37: // Left
				shift = -1; moveAside = true;
				break;
			case 39: // Right
				shift = 1; moveAside = true;
				break;
		}
		
		if (shift != null)
		{
			var method = frameHelper.focusNavBar;
			if (frameHelper.focusArea == null && !frameHelper.isTopMenuMode() && (e.keyCode == 38 || e.keyCode == 40))
				method = frameHelper.focusModule;

			if (frameHelper.focusArea == 1) method = frameHelper.focusModule;
			else if (frameHelper.focusArea == 2) method = frameHelper.focusTopLink;
			else if (frameHelper.focusArea == 3) method = frameHelper.focusMenuLink;
			else if (frameHelper.focusArea == 4) method = frameHelper.focusWorkspace;
			method.call(frameHelper, shift, moveAside, isTab);
			e.preventDefault();
		}
	});

	//---------------------------------------------------------------------------
	$("#linkLogo, #linkLogoM, #linkLogoT").on("click", function (e)
	{
		frameHelper.openUrl($("#linkLogo").attr("data-url"), e);
	});

	$("#navBarMaster").on("selectstart", function (e)
	{
		if (e.target.tagName != "INPUT") e.preventDefault();
	});

	$("#navBarMaster, #navBarDesign, #frameShadow").on("click", function (e)
	{
		if (e.clientX != null) frameHelper.clearNavMenuFocus();
	});

	$("#linkMenu").on("mouseup", function (e)
	{
		var visible = frameHelper.isNavMenuVisible();
		frameHelper.showNavMenu(!visible);
		if (!visible)
		{
			if (frameHelper.__activeModuleID == null && __siteMap.Workspaces)
				frameHelper.__activeModuleID = __siteMap.Workspaces[0].WorkspaceID;
			frameHelper.setActiveModule(frameHelper.__activeModuleID);
			frameHelper.menuWasClicked = true;
		}
	});

	$("#btnMinLeftMenu").on("click", function (e)
	{
		frameHelper.setLeftMenuState(!frameHelper.isLeftMenuMinimized());
	});

	$("#btnCloseNavMenu, #frameShadow, #navBarMaster").on("click", function (e)
	{
		if (!frameHelper.menuWasClicked && !frameHelper.__designMode &&
				!frameHelper.isMenuConfigMode() &&  $(e.target).parents(".navbar-form").length == 0)
		{
			frameHelper.hideNavMenu();
		}
		delete frameHelper.menuWasClicked;
	});

	// workspaces filter event handler
	$("#btnFilterWS").on("click", function (e)
	{
		var target = $(e.target), showAll = target.hasClass("active"), ws;
		target.toggleClass("active");
		frameHelper.renderModules(showAll);

		if (!showAll && ((ws = frameHelper.getActiveModule()) != null) && !ws.Favorite)
		{
			frameHelper.clearActiveModule();
			frameHelper.showModuleMenu(false);
		}
	});

	$("#navMenu").on("mousedown", function (e)
	{
		var isFav = (e.target.tagName == "I" && $(e.target).hasClass("fav-icon"));
		if (!isFav && !frameHelper.__designMode && !frameHelper.isMenuConfigMode())
			frameHelper.handleNavMenuClick(e);
	});
	
	$("#navMenu").on("contextmenu", function (e)
	{
		e.preventDefault();
		//if ($("#scrLocalMenu").is(":visible")) $("body").css("pointer-events", "none");
	});

	//---------------------------------------------------------------------------
	// switches the module's menu view mode
	$("#moduleText").on("click", function (e)
	{
		if ($("#navMenu").hasClass("not-mixed")) return;

		var all = frameHelper.__showAllItems = !frameHelper.__showAllItems;
		$("#moduleFilter").toggleClass("ac-keyboard_arrow_down ac-keyboard_arrow_up");

		$("#spanAllItems").html(all ? frameHelper.__strViewFavorites : frameHelper.__strViewAllItems);
		frameHelper.setShowAllMode();
		frameHelper.renderModuleMenu(frameHelper.__activeModuleID, frameHelper.getSearchStr());
	});

	$("#searchInput").on("input", function (e)
	{
		if (frameHelper.__designMode)
		{ 
			return;
		}
		var inSearchMode = $("#navMenu").hasClass("search-mode");
		if(!$(this).val().length && !inSearchMode)
		{ 
			return; 
		}

		if (!$("#navMenu").is(":visible") || !inSearchMode)	
		{ 
			frameHelper.setSearchMode('menu');
		}

		delete frameHelper.hasSearchResults;
		if (frameHelper.__searchMode == 'menu')
		{
			setTimeout(function () { frameHelper.performSearch(); }, 1);
		}


		//if (frameHelper.__searchMode != 'menu')
		//{
		//	clearTimeout(frameHelper.__searchH);
		//	frameHelper.__searchH = setTimeout(function () { frameHelper.performSearch(); }, 500);
		//}
		//else setTimeout(function () { frameHelper.performSearch(); }, 1);
	});

	$("#searchInput").on("keydown", function (e)
	{
		if (e.keyCode == 13)
		{
			var mode = frameHelper.__searchMode;
			if (frameHelper.hasSearchResults != null || mode == 'menu')
			{
				switch (frameHelper.__searchMode)
				{
					case 'menu': frameHelper.setSearchMode('entity'); break;
					case 'entity': frameHelper.setSearchMode('article'); break;
					case 'article': frameHelper.setSearchMode('file'); break;
					case 'file': frameHelper.setSearchMode('menu'); break;
				}
			}
			frameHelper.performSearch();
		}
		else if (e.keyCode == 9)
		{
			frameHelper.focusNavBar(e.shiftKey ? -1 : 1, true, true);
			e.preventDefault(); this.blur();
		}
		else if (e.keyCode == 40)
		{
			if (frameHelper.isNavMenuVisible()) frameHelper.focusTopLink(1, true);
			else frameHelper.focusModule(1, false);
			e.preventDefault(); this.blur();
		}
		//e.stopPropagation();
	});

	$("#searchControl > div").on("click", function (e)
	{
		if (frameHelper.__searchMode != 'menu') frameHelper.performSearch();
	});

	$("#main").on("load", function (e)
	{
		var frame = e.target, loc, win, mainDoc, screenID;
		try 
		{
			win = frame.contentWindow; loc = win && win.location;
			mainDoc = win && win.document;
		} 
		catch (ex) { }

		if (mainDoc == null) { frameHelper.stopLoadAnimation(); return; }
		window.document.title = mainDoc.title;
		delete frameHelper.screenHelpData;

		if (window.loginUrl && loc.href.indexOf(window.loginUrl) > 0)
		{
			window.top.location = window.top.location.href;
			return;
		}

		window.__activeListUrl = win.__listUrl;
		window.__activeUrl = frameHelper.getScreenUrl(loc.href);
		frameHelper.stopLoadAnimation();
		frameHelper.markActiveModuleW();

		if (frameHelper.needPushState)
		{
			setTimeout(function () { frameHelper.pushMenuState(window.__activeUrl); }, 1);
			delete frameHelper.needPushState;
		}
		win.addEventListener("unload", function (e) {
			frameHelper.hideNavMenu(); frameHelper.hideHelpFrame();
		});

		$(mainDoc.documentElement).on('keydown', handleCtrlQ);

		var path = win.location.pathname;
		if (path && !win.__urlUpdated && __px(win) != null) __px(win).updateBrowserUrl();
	});

	//---------------------------------------------------------------------------
	// user menu click handler
	$("#userMenu").on("click", function (e)
	{
		var target = $(e.target).closest('LI'), cmd = target.attr("data-cmd");
		switch (cmd ? cmd : $(target).attr("role"))
		{
			case "sign-out":
				$.postStd('frameset/auth/logout', function (data, code, xhr)
				{
					if (xhr.status == 204 || xhr.status == 200) window.top.location.reload(true);
				}, 'json');
				break;

			case "bdate":
				$("#btnBusinessDate[data-toggle]").trigger("click");
				e.stopPropagation();
				break;

			case "help":
				$("#btnHelp[data-toggle]").trigger("click");
				e.stopPropagation();
				break;

			case "checkbox":
				frameHelper.changeCompany($(target).text().trim());
				break;
		}
	});

	$("#btnSignOut").on("click", function (e)
	{
		$.postStd('frameset/auth/logout', function (data, code, xhr)
		{
			if (xhr.status == 204 || xhr.status == 200) window.top.location.reload(true);
		}, 'json');
	});

	$("#btnUserProfile").on("click", function (e)
	{
		frameHelper.openUrl($("#btnUserProfile").attr("data-url"), e);
	});

	// help frame load event handler
	$("#help").on("load", function (e)
	{
		frameHelper.stopHelpLoadAnimation();
		delete frameHelper.lastArticle;

		var frame = e.target, win;
		try { win = frame.contentWindow; } catch (ex) { }
		if (win)
		{
			var pair = px.getQueryParam("pageID", win.location.href);
			win.__suppressUrlUpdate = true;
			if (pair) frameHelper.lastArticle = pair.split('=')[1];
		}

		var tree = px_alls['helpTree'];
		if (tree && win) frameHelper.syncHelpTree(win.__nodeGuid);
	});

	$("#helpPathCont").on("click", function (e)
	{
		var href = $(e.target).closest('a').attr('data-href');
		if (href) px.openUrl(href, 'help');
	});

	$("#helpButtons").on("mousedown", function (e)
	{
		var btn = $(e.target).closest('i'), data = frameHelper.screenHelpData;
		switch (btn.attr('data-cmd'))
		{
			case 'close-help':
				frameHelper.hideHelpFrame();
				break;
			case 'quick-help':
				if (data)
				{
					delete data.articleSelected; delete data.articleOpened;
					frameHelper.hideHelpFrame();
					$("#btnHelp").trigger("click");
				}
				break;
		}
	});

	$("#btnShowWiki").on("click", function (e)
	{
		var wikiMode = $("#helpCont").hasClass("wiki"), data = frameHelper.screenHelpData;	
		if (wikiMode) 
		{
			if (data && !data.articleOpened)
			{
				if (data.Wiki) frameHelper.showHelpArticle(data);
				else $("#helpCont div.wikies > div a:first").trigger("click");
			}
			frameHelper.hideWikiFrame();
		}
		else frameHelper.showWikiFrame();
	});

	//---------------------------------------------------------------------------
	// setup menu click handler
	$("#setupMenu").on("click", function (e)
	{
		var target = e.target.tagName == 'LI' ? e.target : e.target.parentNode;
		var cmd = $(target).attr("data-cmd"), msg = window.localizedMessages;

		switch (cmd)
		{
			case "edit-menu":
				frameHelper.setDesignMode(true);
				break;

			case "dock-top":
				var isOnTop = $(target).attr('data-mode') == 'top';
				var params = { SiteMapPosition: isOnTop ? "left" : "top" };
				$.putJSON('frameset/userPreferences', params, function (data, code, xhr)
				{
					if (xhr.status == 200)
					{
						$(target).attr('data-mode', params.SiteMapPosition);
						if (msg) $(target).children('a').text(isOnTop ? msg.dockOnTop : msg.dockOnLeft);
						$("form").toggleClass("top-menu");

						if (isOnTop) { frameHelper.showNavMenu(false); frameHelper.adjustNavMenu(); }
						frameHelper.clearActiveModule();
						frameHelper.showModuleMenu(false);
					}
				});
				break;

			case "expand":
				frameHelper.setLeftMenuState(false);
				break;
			case "full-menu":
				frameHelper.renderModules();
				break;
		}
	});

	// workspace favorite button click handler
	$("#btnFavoriteWS").on("click", function (e)
	{
		frameHelper.onFavoriteClick(e, frameHelper.__activeModuleID);
	});

	$("#btnEnterMenuConfig").on("click", function (e)
	{
		frameHelper.setMenuConfigMode(true);
		e.stopPropagation();
	});

	$("#exitMenuConfig").on("click", function (e)
	{
		frameHelper.setMenuConfigMode(false);
		e.stopPropagation();
	});

	// design mode handlers
	$("#btnExitDesign").on("click", function (e)
	{
		frameHelper.setDesignMode(false);
	});

	$("#btnNewWS").on("click", function (e)
	{
		frameHelper.hideAllPanels();
		frameHelper.showPanel("pnlWorkspace");
	});

	$("#btnNewScreen").on("click", function (e)
	{
		if (frameHelper.__activeModuleID != null)
		{
			frameHelper.selectedScreens = [];
			frameHelper.renderScreensToSelect();
			frameHelper.hideAllPanels();
			frameHelper.showPanel("pnlAddScreen");
		}
	});
	$("#btnNewTile").on("click", function (e)
	{
		if (frameHelper.__activeModuleID != null)
		{
			frameHelper.hideAllPanels();
			frameHelper.showPanel("pnlNewTile");
		}
	});



	frameHelper.registerDropDown("txtWsIcon", structuredIcons, IconsList);
	frameHelper.registerDropDown("txtTileIcon", structuredIcons, IconsList);
	frameHelper.registerDropDown("txtCatIcon", structuredIcons, IconsList);
	frameHelper.registerDropDown("txtTileScreen", null, ScreensList);
	frameHelper.registerDropDown("txtCompany", __companies, StringList);

	//---------------------------------------------------------------------------
	// append workspace handler
	$("#btnOkWs, #btnCancelWs").on("click", function (e)
	{
		if (e.target.id == "btnOkWs" && $("#txtWsTitle").val())
		{
			var params = new Object(), id = $("#pnlWorkspace").attr("data-params");
			params.Title = $("#txtWsTitle").val();
			params.Icon = $("#txtWsIcon").val();
			params.AreaID = frameHelper.getControlValue("txtWsArea");

			var wsList = __siteMap.Workspaces;
			if (id)
			{
				var item = wsList.find(function (n) { return n.WorkspaceID == id });
				if (item != null)
					params.Deletable = item.Deletable;

				$.postJSON('frameset/workspace/' + id, params, function (data, code, xhr)
				{
					if (xhr.status == 204)
					{
						if (item != null)
						{
							item.Title = params.Title; item.Icon = params.Icon; item.AreaID = params.AreaID;
							frameHelper.renderModules();
							frameHelper.reloadSiteMap = true;
						}
					}
				});
			}
			else
			{
				$.postJSON('frameset/workspace', params, function (data, code, xhr)
				{
					if (xhr.status == 201)
					{
						var ws = JSON.parse(xhr.responseText), scroll = $("#leftMenuW");
						if (ws.Subcategories == null) ws.Subcategories = new Array();
						if (ws.Tiles == null) ws.Tiles = new Array();

						wsList.push(ws);  frameHelper.renderModules();
						frameHelper.setActiveModule(ws.WorkspaceID);
						scroll.scrollTop(scroll.prop('scrollHeight') - scroll.prop('offsetHeight'));
						frameHelper.reloadSiteMap = true;
					}
				});
			}
		}
		frameHelper.hidePanel("pnlWorkspace");
	});

	// append screen handler
	$("#btnOkAs, #btnAddAs, #btnCancelAs").on("click", function (e)
	{
		if (e.target.id != "btnAddAs") frameHelper.hidePanel("pnlAddScreen");

		if (e.target.id != "btnCancelAs")
		{
			var list = frameHelper.selectedScreens;
			var owner = $("#listAddScreen")[0];

			if (list && list.length) $.postJSON('frameset/workspace/' + frameHelper.__activeModuleID + "/add", list,
				function (data, code, xhr)
				{
					if (xhr.status == 200)
					{
						__siteMap.Workspaces[frameHelper.getActiveWsNumber()] = JSON.parse(xhr.responseText);
						frameHelper.renderModuleMenu(frameHelper.__activeModuleID);
						frameHelper.reloadSiteMap = true;
					}
					if (e.target.id == "btnAddAs")
					{
						frameHelper.selectedScreens = [];
						frameHelper.renderScreensToSelect($("#txtSearchScreen").val());
					}
				});
		}
	});

	// screens filter
	$("#txtSearchScreen").on("input", function (event)
	{
		setTimeout(function ()
		{
			frameHelper.renderScreensToSelect($("#txtSearchScreen").val());
		}, 1);
	});

	//----------------------------------------------------------------------------
	// append tile handler
	$("#btnOkTile, #btnCancelTile").on("click", function (e)
	{
		if (e.target.id == "btnOkTile" && $("#txtTileTitle").val())
		{
			var params = new Object(), id = $("#pnlNewTile").attr("data-params");
			params.ScreenID = frameHelper.getControlValue("txtTileScreen");
			params.Title = frameHelper.getControlValue("txtTileTitle");
			params.Icon = frameHelper.getControlValue("txtTileIcon");
			params.Parameters = frameHelper.getControlValue("txtTileParams");

			var baseUrl = 'frameset/workspace/' + frameHelper.__activeModuleID + '/tiles';
			var ws = frameHelper.getActiveModule();
			var tiles = frameHelper.getTiles(ws);
			if (id)
			{
				$.postJSON(baseUrl + '/' + id, params, function (data, code, xhr)
				{
					if (xhr.status == 204)
					{
						var item = tiles.find(function (n) { return n.TileID == id });
						if (item)
						{
							for (var n in params) item[n] = params[n];
							frameHelper.renderTopLinks();
							frameHelper.reloadSiteMap = true;
						}
					}
				});
			}
			else if (params.Title && params.ScreenID)
			{
				$.postJSON(baseUrl, params, function (data, code, xhr)
				{
					if (xhr.status == 201)
					{
						tiles.push(JSON.parse(xhr.responseText));
						frameHelper.renderTopLinks();
						frameHelper.reloadSiteMap = true;
					}
				});
			}
		}
		frameHelper.hidePanel("pnlNewTile");
	});

	//----------------------------------------------------------------------------
	// append category handler
	$("#btnAddCategory").on("click", function (e)
	{
		frameHelper.setCategoryDialogTip(null);
		frameHelper.showPanel("pnlCategory");
	});

	$("#btnOkCat, #btnCancelCat").on("click", function (e)
	{
		if (e.target.id == "btnOkCat" && $("#txtCatTitle").val())
		{
			var params = new Object(), id = $("#pnlCategory").attr("data-params");
			params.Name = $("#txtCatTitle").val();
			params.Icon = $("#txtCatIcon").val();
			//params.Column = $("#ddCatColumn").val();

			if (id)
			{
				var item = __categories.find(function (n) { return n.SubcategoryID == id });
				if (item)
					params.Deletable = item.Deletable;

				$.postJSON('frameset/subcategory/' + id, params, function (data, code, xhr)
				{
					if (xhr.status == 204)
					{
						if (item)
						{
							item.Icon = params.Icon; item.Name = params.Name; item.Column = params.Column;
							frameHelper.renderCategories();
							frameHelper.categoriesChanged = true;
							frameHelper.reloadSiteMap = true;
						}
					}
				});
			}
			else
			{
				$.postJSON('frameset/subcategory', params, function (data, code, xhr)
				{
					if (xhr.status == 201)
					{
						__categories.push(JSON.parse(xhr.responseText));
						frameHelper.renderCategories();
						frameHelper.categoriesChanged = true;
						frameHelper.reloadSiteMap = true;
					}
				});
			}
		}
		e.stopPropagation();
		frameHelper.hidePanel("pnlCategory");
	});

	// reset menu event handler
	$("#btnResetMenu").on("click", function (e)
	{
		var module = frameHelper.__activeModuleID;
		frameHelper.showConfirm(frameHelper.getLocalString("confirmDeleteSetup"),
			function ()
			{
				$.postStd('frameset//sitemap/resetToDefault', function (data, code, xhr)
				{
					if (xhr.status == 200)
					{
						frameHelper.getSiteMap(function ()
						{
							if (module != null) frameHelper.renderModuleMenu(module);
							frameHelper.renderCategories();
							frameHelper.reloadSiteMap = true;
						}, true);
					}
				});
			});
	});

	//----------------------------------------------------------------------------
	// edit screen event handler
	$("#btnOkScreen, #btnCancelScreen").on("click", function (e)
	{
		if (e.target.id == "btnOkScreen" && $("#txtScreenTitle").val())
		{
			var id = $("#pnlScreen").attr("data-params"), pair;

			if (id && (pair = frameHelper.getScreenInActiveWS(id, true)) != null)
			{
				var url = 'frameset/screen/' + id, item = pair[0];
				item.Title = $("#txtScreenTitle").val();

				var site = px.getSiteName();
				if (site && item.Url.toLowerCase().indexOf(site.toLowerCase()) == 0)
				{
					item.Url = item.Url.substring(site.length);
					if (item.Url[0] != '~') item.Url = '~' + item.Url;
				}

				var anySuccess = 0, count = 1;
				var success = function ()
				{
					if (anySuccess == count)
						frameHelper.getSiteMap(function ()
						{
							frameHelper.renderModuleMenu(frameHelper.__activeModuleID);
							frameHelper.reloadSiteMap = true;
						}, true);
				};

				var batch = $.batch();

				var newCategory = $("#ddScreenCat").val();
				if (pair[1].SubcategoryID != newCategory)
				{
					count += 1;
					batch.add(function()
					{
						$.postStd(url + '/changeSubcategory?subcategoryId=' + newCategory,
							function(data, code, xhr)
							{
								if (xhr.status == 204) { anySuccess++; success(); }
							});
					});
				}

				batch.add(function()
				{
					$.postJSON(url, item, function(data, code, xhr)
					{
						if (xhr.status == 204) { anySuccess++; success(); }
					});
				});

				batch.send();
			}
		}
		frameHelper.hidePanel("pnlScreen");
	});

	// setup menu click handler
	$("#navBarSearch").on("click", function (e)
	{
		var target = $(e.target).parents('li');
		if (e.clientX != null) frameHelper.clearNavMenuFocus();

		if (target && !target.hasClass("pushed"))
			frameHelper.performSearch(target.attr("data-cmd"));
	});

	$("#btnBdOK").on("click", function (e)
	{
		var date = px_alls["bdCalendar"].getSelectedDate();
		frameHelper.setBusinessDate(date);
	});
}


//=============================================================================
// Helper methods
//=============================================================================

var frameHelper =
{
	__strViewAllItems: "View All",
	__strViewFavorites: "Favorites",
	__systemParams: ["screenid", "companyid", "timestamp", "hidescript", "popuppanel"],
	__searchMode: "menu",
	__searchUrl: "frameset/search/",
	__favoriteID: "AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE",
	__defHelpArticle: "00000000-0000-0000-0000-000000000000",
	__moreItemsID: -1,
	
	selectedScreens: [],
	activePanels: new Array(),

	getBusinessDate: function()
	{
		return new Date($("#btnBusinessDate").attr("data-date"));
	},
	setBusinessDate: function (date)
	{
		var text = date.format('d', _dateFormatInfo);
		var invText = px.getInvariantText(date);
		$.postJSON('frameset/userPreferences/businessDate', invText, function (data, code, xhr)
		{
			if (xhr.status == 204)
			{
				$("#btnBusinessDate span.bd-date").html(text);
				$("#btnBusinessDate").attr("data-date", invText);

				var frMain = $("#main")[0];
				if (frMain) px.openUrl(frMain.contentWindow.location.href, "main");
			}
		});
	},

	getControlValue: function(name)
	{
		var target = $('#' + name), val = target.attr('data-value');
		return val != null ? val : target.val();
	},

	getLocalString: function(name, p1, p2)
	{
		var lm = window.localizedMessages, str = lm[name];
		if (p1)
		{
			var lp1 = p1;
			if (lm[p1]) lp1 = lm[p1];
			str = str.replace('{0}', lp1);
		}
		if (p2)
		{
			var lp2 = p2;
			if (lm[p2]) lp2 = lm[p2];
			str = str.replace('{1}', lp2);
		}
		return str;
	},

	//---------------------------------------------------------------------------
	// methods to work with help
	hideHelpFrame: function()
	{
		$("#bodyContent").removeClass("help");
		$("#btnHelp").parent().removeClass("open");
		$("#bodyShadow").hide();
		frameHelper.activePopup = null;
	},

	showHelpFrame: function(data, showWikies)
	{
		if (data == null) data = this.screenHelpData;
		if (data == null && !showWikies) return;

		$("#searchInput").val("");
		this.hideNavMenu();
		showWikies ? this.showWikiFrame() : this.hideWikiFrame();

		if (data && data.Html && !data.articleSelected)
		{
			var doc = $("#quickHelp")[0].contentWindow.document;
			doc.open();
			doc.write(data.Html);
			doc.close();
			$("#pnlQuickHelp").css("display", "");
			this.setQuickHelpSize();

			doc = doc.documentElement;
			$(doc).on("click", function (e) { frameHelper.onHelpLinkClick(e); });
			$(doc).on("contextmenu", function (e) { e.preventDefault(); });
			return;
		}

		$("#bodyContent").addClass("help");
		$("#btnHelp").parent().addClass("open");
		$("#bodyShadow").hide();
		$("#btnQuickHelp").toggle(data && !!data.Html);

		this.activePopup = $("#helpCont")[0];
		if (!this.wikiFrameVisible()) this.showHelpArticle(data);
	},

	isHelpFrameVisible: function ()
	{
		return $("#bodyContent").hasClass("help");
	},
	refreshHelpTree : function()
	{
		var tree = px_alls['helpTree'];
		if (tree) tree.refresh();
	},

	showHelpArticle: function(data)
	{
		var tree = px_alls['helpTree'], articleChanged, eventName;
		var handler = function ()
		{
			frameHelper.startHelpLoadAnimation();
			px.openUrl(data.Url, 'help');
			tree.events.removeEventHandler(eventName, handler);
			data.articleOpened = true;
		};

		if (!data.articleOpened && this.lastArticle != data.Article)
		{
			this.hideWikiFrame(false);
			data.Url = px.replaceQueryParam("pageID", data.Url, data.Article);
			articleChanged = true;
		}

		if (tree.startNodePath != data.Wiki)
		{
			if (articleChanged) tree.events.addEventHandler(eventName = "afterRefresh", handler);
			tree.startNodePath = data.Wiki; tree.refresh(data.Article);
			tree.wikiName = data.WikiName;
		}
		else
		{
			if (articleChanged) tree.events.addEventHandler(eventName = "afterSync", handler);
			tree.nodes.collapse(true); this.syncHelpTree(data.Article);
		}
	},

	showHelpArticleByID: function(id, storeHelpData)
	{
		$.getStd("ui/help/wiki?articleID=" + id,
			function (data, code, xhr)
			{
				if (xhr.status != 200) return;
				var wiki = { Wiki: data.PageID, Article: id, WikiName: data.Title, Url: data.DefaultUrl };
				if (storeHelpData) frameHelper.screenHelpData = wiki;
				frameHelper.showHelpFrame(wiki, frameHelper.isDefaultHelpArticle(id));
			});
	},

	setQuickHelpSize: function()
	{
		var pnl = $("#pnlQuickHelp");
		if (pnl.is(":visible")) pnl.css("height", $("#bodyContent").height() + "px");
	},

	showWikiFrame: function()
	{
		$("#btnShowWiki").removeClass("ac-keyboard_return");
		$("#btnShowWiki").addClass("ac-subdirectory_arrow_right");
		$("#helpCont").addClass("wiki");

		if (!this.wikiesLoaded)
		{
			$.getStd("ui/help/wikies", function (data, code, xhr)
			{
				if (xhr.status != 200) return;
				frameHelper.wikies = data;
				ReactDOM.render(React.createElement(WikiLinks, { items: data }), $("#wikiCont")[0]);
				frameHelper.wikiesLoaded = true;
			});
		}
	},
	hideWikiFrame: function ()
	{
		$("#btnShowWiki").removeClass("ac-subdirectory_arrow_right");
		$("#btnShowWiki").addClass("ac-keyboard_return");
		$("#helpCont").removeClass("wiki");
	},
	wikiFrameVisible: function()
	{
		return $("#helpCont").hasClass("wiki");
	},

	//---------------------------------------------------------------------------
	syncHelpTree: function(pageID)
	{
		var tree = px_alls['helpTree'];
		if (!tree.helpPathAttached)
		{
			var sync = function (sender, args) {
				tree.renderPath($('#helpPathCont')[0], 'helpPath', null, null, tree.wikiName);
			};
			tree.helpPathAttached = true;
			tree.events.addEventHandler("afterSync", sync);
		}
		tree.sync(pageID);
	},

	isDefaultHelpArticle: function(id)
	{
		return id == this.__defHelpArticle;
	},

	onHelpClick: function (e, isOpen, $this)
	{
		if (isOpen)
		{
			var alt = e && (e.ctrlKey || e.altKey), win = this.getMainFrameWin();
			var scr = win ? win.__screenID : "00.00.00.00", url = window.__activeUrl;

			if (url && url.indexOf("default") == 0) scr = "DB.00.00.00";
			if (scr && !scr.replace(/\./g, '').trim()) scr = "00.00.00.00";

			if (this.screenHelpData)
				this.showHelpFrame(this.screenHelpData, this.wikiFrameVisible());
			else if (scr)
			{
				$.getStd("ui/help/" + scr.replace(/\./g, ''), function (data, code, xhr)
				{
					if (xhr.status == 200)
					{
						if (!data.Html && !alt)
						{
							frameHelper.hideHelpFrame();
							px.openFrameset("about:blank", null, "wikiPage=" + data.Article);
						}
						else
						{
							frameHelper.showHelpFrame(
								frameHelper.screenHelpData = data, frameHelper.isDefaultHelpArticle(data.Article));
						}
					}
				});
			}
			else this.hideHelpFrame();
		}
		else
		{
			this.hideHelpFrame();
			$("#pnlQuickHelp").css("display", "none");
		}
	},

	onHelpLinkClick: function (e)
	{
		var a = $(e.target).closest('A'), href, help = this.screenHelpData;
		var alt = (e.ctrlKey || e.altKey);

		if (a.length && (href = a.attr('href')) != null)
		{
			if (href == "contents" && help)
			{
				if (!alt)
				{
					frameHelper.hideHelpFrame();
					px.openFrameset("about:blank", null, "wikiPage=" + this.__defHelpArticle);
				}
				else
				{
					help.articleSelected = true;
					window.clearDropDownMenus();
					this.showHelpFrame(help, true);
				}
			}

			var wikiName = px.getQueryParamVal("wikiName", href);
			var pageID = px.getQueryParamVal("pageID", href);

			if (pageID && !alt)
			{
				window.clearDropDownMenus();
				px.openFrameset("about:blank", null, "wikiPage=" + pageID);
			}
			else if (wikiName && pageID)
			{
				$.getStd("ui/help/wiki?name=" + wikiName, function (data, code, xhr)
				{
					help.Wiki = data.PageID; help.WikiName = data.Title;
					help.Article = pageID;
					//help.Url = href + "&HidePageTitle=on";
					help.articleSelected = true;
					
					window.clearDropDownMenus();
					frameHelper.showHelpFrame(help);
				});
			}
			e.preventDefault();
		}
	},

	getWikiName: function(id)
	{
		if (frameHelper.wikies == null) return null;
		var item = frameHelper.wikies.find(function (n) { return n.PageID == id });
		return item ? item.Title : null;
	},

	//---------------------------------------------------------------------------
	// the time zone watch
	digitalWatch: function ()
	{
		var now = new Date();
		var date = new Date(now.getTime() + now.getTimezoneOffset() * 60 * 1000);
		if (window.timeShift) date.setTime(date.getTime() + window.timeShift);

		var btn = $("#btnBusinessDate span.bd-time");
		if (btn.length)
		{
			window.localTime = date.format('t', _dateFormatInfo);
			btn.text(window.localTime);
			this.digitalWatchT = setTimeout("frameHelper.digitalWatch()", 5000);
		}
		else setTimeout("frameHelper.digitalWatch()", 300);
	},

	onBusinessDateClick: function (e, isOpen, $this)
	{
		this.onDropDownBtnClick(e, isOpen, $this);
		if (isOpen) px_alls["bdCalendar"].setSelectedDate(this.getBusinessDate());
	},

	onSetupMenuClick: function (e, isOpen, $this)
	{
		this.onDropDownBtnClick(e, isOpen, $this);
		if (!isOpen)
		{
			if (this.categoriesChanged)
				frameHelper.getSiteMap(function () { frameHelper.renderModuleMenu(frameHelper.__activeModuleID); }, true);
		}
		else
		{
			this.renderCategories();
			frameHelper.categoriesChanged = false;
		}
	},

	onDropDownBtnClick: function (e, isOpen, $this)
	{
		var img = $this.children("i.arrow");
		img.removeClass("ac-expand_more ac-expand_less");
		isOpen ? img.addClass("ac-expand_less") : img.addClass("ac-expand_more");
	},

	changeCompany: function(company)
	{
		$.postJSON('frameset/auth/switchCompany?name=' + company, null,
			function (data, code, xhr)
			{
				frameHelper.switchCompany(company, data);
			});
	},

	switchCompany: function(company, reload)
	{
		$("#btnUserMenu span.company-name").html(company);

		var url = px.removeUrlParam(window.location.href, "CompanyID");
		var query = url.split('?').length > 1 ? url.split('?')[1] : "";
		window.top.history.replaceState({}, document.title, '?' + query);

		if (reload)
		{
			window.top.location.reload(true);
		}
		else
		{
			this.hideNavMenu();
			this.clearActiveModule();

			this.refreshCompanyLogo();
			this.getSiteMap();
			$("#main").attr("src", $("#main").attr("src"));
		}
	},

	isTopMenuMode: function ()
	{
		return $("form").hasClass("top-menu");
	},
	isMenuConfigMode: function()
	{
		return $("#navMenu").hasClass("config-mode");
	},
	isNavMenuVisible: function ()
	{
		if (this.isTopMenuMode()) return $("#menuCont").is(":visible");
		return $("#navMenu").is(":visible");
	},
	isFavoritesVisible: function()
	{
		return $("#navMenu").hasClass("fav-mode");
	},

	getActiveModule: function()
	{
		var result = this.getWs(this.__activeModuleID);
		return result;
	},
	getWs: function (id)
	{
		var upper = (id + "").toUpperCase();
		return __siteMap.Workspaces && __siteMap.Workspaces.filter(function (ws) { return (ws.WorkspaceID+"").toUpperCase() == upper }).shift();
	},
	getActiveWsNumber: function ()
	{
		return __siteMap.Workspaces && __siteMap.Workspaces.indexOf(this.getActiveModule());
	},
	getSubcats: function (ws)
	{
		return ws && (ws.Subcategories = ws.Subcategories || []);
	},
	getTiles: function (ws)
	{
		return ws && (ws.Tiles = ws.Tiles || []);
	},

	isFavorite: function (id)
	{
		return (id + "").toUpperCase().trim() == this.__favoriteID;
	},
	favoritesSelected: function ()
	{
		return this.isFavorite(this.__activeModuleID);
	},
	isMoreItems: function (id)
	{
		return id == this.__moreItemsID;
	},
	isShowOnlyPin: function ()
	{
		var activeWS = (this.__activeModuleID != this.__moreItemsID) && this.__activeModuleID;
		return !frameHelper.__showAllItems && !frameHelper.__designMode &&
			!this.isMenuConfigMode() && activeWS && this.moduleHasMixedContent(activeWS);
	},

	// checks if workspace has any links
	isModuleHasLinks: function (index)
	{
		var ws = this.getWs(index), items = this.getSubcats(ws);

		if (items) for (var i = 0; i < items.length; i++)
				if (items[i].Screens.length > 0) return true;

		var tiles = this.getTiles(ws);
		return tiles && tiles.length > 0;
	},

	// stores menu state in browser history
	pushMenuState: function(url)
	{
		var ws = this.getScreenModule(url);
		window.history.pushState(null, window.document.title, "");
	},

	setShowAllMode: function()
	{
		var cssOwner = $("#navMenu"), all = !this.isShowOnlyPin();
		all ? cssOwner.addClass("show-all") : cssOwner.removeClass("show-all");

		var mixed = this.moduleHasMixedContent(this.__activeModuleID);
		mixed ? cssOwner.removeClass("not-mixed") : cssOwner.addClass("not-mixed");
	},

	//---------------------------------------------------------------------------
	// finds the screen with specified id in active workspace
	getScreenInActiveWS: function (id, withCategory)
	{
		var list = this.getSubcats(this.getActiveModule());
		var cat = null, screen = null;
		if (list) for (var i = 0; i < list.length; i++)
		{
			var item = list[i].Screens.find(function (n) { return n.NodeID == id });
			if (item) { screen = item; cat = list[i]; break; }
		}
		if (screen == null) return null;
		return withCategory ? [screen, cat] : screen;
	},

	getScreenByID: function(id)
	{
		var wsList = __siteMap.Workspaces.slice(0), item, subCut;
		wsList.push({ Subcategories: __siteMap.UnassignedScreens.slice(0) });
		wsList.push({ Subcategories: [{ Screens: __siteMap.ScreensAvailableForTiles.slice(0) }] });
		id = id.toLowerCase();
		wsList.find(function (ws)
		{
			subCut = frameHelper.getSubcats(ws).find(function (sc)
			{
				item = sc.Screens.find(function (n) { return n.ScreenID.toLowerCase() == id; });
				return item != null;
			});
			return subCut != null;
		});
		return item;
	},

	getAreaByID: function(id)
	{
		id = id.toLowerCase();
		return __siteMap.Areas.find(function (area) { return area.AreaID.toLowerCase() == id; });
	},

	getScreenModule: function(url)
	{
		if (!window.__siteMap || !__siteMap.Workspaces || !url) return null;

		url = url.toLowerCase();
		for (var i = 1; i < __siteMap.Workspaces.length; i++)
		{
			var ws = __siteMap.Workspaces[i];
			var subcats = this.getSubcats(ws);
			for (var j = 0; j < subcats.length; j++)
			{
				var item = subcats[j].Screens.find(
					function (n) { return frameHelper.getScreenUrl(n.Url).indexOf(url) >= 0; });
				if (item != null) return ws;
			}
		}
		return null;
	},

	getScreenUrl: function (url)
	{
		var start = url.lastIndexOf('/') + 1, end = url.indexOf('?', start);
		if (end < 0) return url.substring(start).toLowerCase();

		var query = url.split('?')[1], query2 = new Array();
		if (query)
		{
			var params = query.split('&');
			for (var i = 0; i < params.length; i++)
			{
				var pair = params[i].split('='), name = pair[0].toLowerCase();
				if (this.__systemParams.indexOf(name) < 0) query2.push(params[i]);
			}
		}

		if (query2.length == 0) return url.substring(start, end).toLowerCase();
		return (url.substring(start, end) + '?' + query2.join('&')).toLowerCase();
	},

	resolveUrl: function (url)
	{
		if (url[0] == '/') url = px.getOrigin() + url;
		return url;
	},

	openUrl: function(url, e, openWin)
	{
		var openTab = (jQuery.type(e) === "boolean") ? e : (e && (e.ctrlKey || e.altKey));
		var openInside = (!openTab && !openWin);

		if (openInside) this.hideNavMenu();
		px.openUrl(url, openWin ? null : "main", openTab, openWin);

		if (openInside)
		{
			var frame = $("#main")[0], win, hasChanges;
			try
			{
				win = frame.contentWindow;
				hasChanges = win && win.px_cm && win.px_cm.checkDataChanges();
			}
			catch (ex) { }

			if (!hasChanges)
			{
				this.startLoadAnimation();
				this.markActiveModule(this.getScreenUrl(url));
			}
		}
	},

	markActiveModule: function(url)
	{
		var owner = $("#listModules"), ws = this.getScreenModule(url);
		owner.children("div.list-group-item").removeClass("selected");
		if (ws) owner.children("div[data-id='" + ws.WorkspaceID + "']").addClass("selected");
		return !!ws;
	},

	getMainFrameWin: function()
	{
		var frame = $("#main")[0], win;
		try { win = frame.contentWindow; } catch (ex) { }
		return win;
	},

	markActiveModuleW: function()
	{
		if (window.__activeUrl)
		{
			var win = this.getMainFrameWin();
			if (!frameHelper.markActiveModule(window.__activeUrl) && win && win.__listUrl)
				frameHelper.markActiveModule(win.__listUrl);
		}
	},

	//---------------------------------------------------------------------------
	// checks if workspace has items with different favourite flag
	moduleHasMixedContent : function(index)
	{
		if (index == null || this.isFavorite(index)) return false;
		var items = this.getSubcats(this.getWs(index)), flag = null;
		
		if (items)
		{
			for (var i = 0; i < items.length; i++)
			{
				for (var j = 0; j < items[i].Screens.length; j++)
				{
					if (flag == null) flag = items[i].Screens[j].Pinned;
					else if (flag != items[i].Screens[j].Pinned) return true;
				}
			}
		}
		return false;
	},

	getSiteMap: function (handler, shared)
	{
		var url = 'frameset/sitemap' + (shared ? "?shared=true" : "");
		$.getStd(url, function (data, code, xhr)
		{
			$("#leftMenu").parent().removeClass("spinner");
			if (xhr.status != 200) return;

			var firstWS = data.Workspaces[0], sm = window.__siteMap;
			if (firstWS && firstWS.WorkspaceID.indexOf("aaaaaaaa") == 0)
				firstWS.Icon = "star_border";

			var fakeWs = sm ? sm.Workspaces.find(function (ws) { return !!ws.FakeFavorite; }) : null;
			window.__siteMap = data;
			if (fakeWs)
			{
				fakeWs = data.Workspaces.find(function (ws) { return ws.WorkspaceID == fakeWs.WorkspaceID; });
				if (fakeWs) fakeWs.FakeFavorite = true;
			}

			if (!frameHelper.getActiveModule()) frameHelper.__activeModuleID = undefined;
			if (shared) window.__siteMapShared = data; else window.__siteMapUser = data;

			if (data)
			{
				window.__categories = data.Subcategories;
				frameHelper.renderModules();
			}
			frameHelper.markActiveModuleW();
			if (handler) handler(data);
		}, 'json');
	},


	getCategories: function ()
	{
		$.getStd('frameset/subcategory', function (data, code, xhr)
		{
			if (xhr.status == 200) window.__categories = data;
		}, 'json');
	},

	showNavMenu: function (visible)
	{
		var shadow = $("#frameShadow");
		if (visible)
		{
			$("#menuCont").css("display", "table");
			shadow.show();
			$("form").addClass("menu-visible");
			this.adjustNavMenu();
		}
		else
		{
			$("#searchInput").val("");
			$("#menuCont").css("display", "");
			if (!shadow.hasClass("spinner")) shadow.hide();
			$("form").removeClass("menu-visible");
		}
	},

	hideNavMenu: function ()
	{
		delete this.searchAutoOpen;
		if (this.focusArea > 0) this.clearNavMenuFocus();
		if (!this.isTopMenuMode()) 
		{
			this.showModuleMenu(false);
			this.clearActiveModule();
		}
		else this.showNavMenu(false);
	},

	setLeftMenuState: function(minimize)
	{
		$("#btnMinLeftMenu").removeClass("ac-keyboard_arrow_left ac-keyboard_arrow_right");
		if (minimize)
		{
			$("#bodyContent").addClass("min"); $("#navBarMaster").addClass("min");
			$("#btnMinLeftMenu").addClass("ac-keyboard_arrow_right");
		}
		else
		{
			$("#bodyContent").removeClass("min"); $("#navBarMaster").removeClass("min");
			$("#btnMinLeftMenu").addClass("ac-keyboard_arrow_left");
			delete this.restoreLeftMenuState;
		}
	},
	isLeftMenuMinimized: function()
	{
		return $("#bodyContent").hasClass("min");
	},

	adjustLeftMenu: function()
	{
		var bodyW = document.documentElement.clientWidth;
		if (bodyW < 580)
		{
			if (!this.isLeftMenuMinimized()) { this.setLeftMenuState(true); this.restoreLeftMenuState = true; }
		}
		else if (this.restoreLeftMenuState) this.setLeftMenuState(false);
	},

	// turns on/off the design mode
	setDesignMode: function(value)
	{
		if (value)
		{
			if (window.__siteMapShared == null)
			{
				this.getSiteMap(function () { frameHelper.setDesignMode(true); }, true);
				return;
			}
			else
			{
				window.__siteMap = window.__siteMapShared;
				window.__categories = window.__siteMap.Subcategories;
			}

			this.setLeftMenuState(false);
			$("#navMenu").removeClass("config-mode");

			$("body").addClass("design");
			this.__designMode = true;
			$("#searchInput").val(""); $("#searchControl").hide();

			var activeWS = this.__activeModuleID;
			if (this.isFavorite(activeWS)||this.isMoreItems(activeWS)) { this.clearActiveModule(); activeWS = null; }

			this.setDesignButtonsState();
			this.renderModules();
			if (activeWS != null) this.renderModuleMenu(activeWS);
			else this.showModuleMenu(false);

			frameHelper.registerDropDown("txtWsArea", __siteMap.Areas, ItemsList, "AreaID", "Name");
		}
		else
		{
			if (frameHelper.reloadSiteMap)
			{
				delete frameHelper.reloadSiteMap;
				this.getSiteMap(function () { frameHelper.setDesignMode(false); }, false);
				return;
			}
			window.__siteMap = window.__siteMapUser;
			window.__categories = window.__siteMap.Subcategories;
			$("body").removeClass("design");
			frameHelper.__designMode = false;
			$("#searchControl").show();

			frameHelper.renderModules();
			if (frameHelper.__activeModuleID != null)
				frameHelper.renderModuleMenu(frameHelper.__activeModuleID);
			else
				frameHelper.showModuleMenu(false);
		}
	},

	setMenuConfigMode: function(value)
	{
		value ? $("#navMenu").addClass("config-mode") : $("#navMenu").removeClass("config-mode");
		if (this.isNavMenuVisible()) this.renderModuleMenu(this.__activeModuleID);
	},

	//---------------------------------------------------------------------------
	// methods to work with modules and categories
	selectModule: function (guid)
	{
		var owner = $("#listModules");
		owner.children("div.list-group-item").removeClass("active");
		owner.children("div[data-id='" + guid + "']").addClass("active");
		this.__activeModuleID = guid;
		if (this.__designMode) this.setDesignButtonsState();
	},

	clearActiveModule: function ()
	{
		var owner = $("#listModules");
		owner.children("div").removeClass("active");
		this.__activeModuleID = null;
		if (this.__designMode) this.setDesignButtonsState();
	},

	showModuleMenu: function (visible)
	{
		var shadow = $("#frameShadow");
		if (visible)
		{
			$("#navMenu").show(); shadow.show();
			$("#listModules").removeClass("show-selected");
		}
		else
		{
			$("#searchInput").val("");
			$("#navMenu").hide();
			if (!shadow.hasClass("spinner")) shadow.hide();

			$("#listModules").addClass("show-selected");
			delete this.searchAutoOpen;
			if (this.isMenuConfigMode()) this.setMenuConfigMode(false);
		}
	},

	renderModuleMenu: function (index, searchStr)
	{
		var owner = $("#moduleMenu")[0], navMenu = $("#navMenu");
		var isMore = this.isMoreItems(index), notFound = !this.getWs(index) && !isMore;

		if (searchStr || notFound)
		{
			this.setSearchStyles(true);
			this.renderTopLinks(-1);
			ReactDOM.render(React.createElement(SearchResult, { searchString: searchStr }), owner);
		}
		else
		{
			this.setSearchStyles(false);
			if (isMore) navMenu.addClass("ws-mode");

			if (this.isFavorite(index) && !this.__designMode) navMenu.addClass("fav-mode");
			if (!this.isModuleHasLinks(index)) navMenu.addClass("empty");

			if (index !== undefined && !isMore)
			{
				var showPin = this.__designMode || (this.isMenuConfigMode() && !this.isFavorite(index));
				$("#moduleFilter").toggle(this.moduleHasMixedContent(index));
				this.renderTopLinks(index);
				ReactDOM.render(React.createElement(ModuleMenu,
					{ index: index, showFav: true, showPin: showPin, width: $("#rightColumn").width() - 130 }), owner);
			}
			else
			{
				this.renderOtherModules();
			}
		}

		setTimeout(function ()
		{
			$("#moduleMenu a.list-group-item").each(function (index, elem)
			{
				if (!searchStr)
				{
					px_tm.registerOverflowTip(elem);
					px_tm.registerElem(elem);
				}
				else if ($(elem).attr(px_tm.attrName) == null)
					px_tm.registerElem(elem);
			});
		}, 1);
	},

	refreshMenu: function()
	{
		frameHelper.reloadSiteMap = true;
		delete frameHelper.needRefreshFav;

		this.refreshCompanyLogo();

		frameHelper.isHelpFrameVisible() ?
			frameHelper.refreshHelpTree() : frameHelper.setDesignMode(false);
	},

	// company logo refresh
	refreshCompanyLogo: function()
	{
		$.getStd("frameset/auth/companyLogo", function (data, code, xhr)
		{
			if (xhr.status == 200) $("#linkLogo > img, #linkLogoT > img").attr("src", data);
		});
	},

	setActiveModule: function(index)
	{
		var ws = this.getWs(index);
		var handler = function ()
		{
			$("#searchInput").val("");
			$("#moduleText > span.text.name").html(ws ? ws.Title : "");
			delete frameHelper.searchAutoOpen;

			frameHelper.selectModule(index);
			frameHelper.setActiveModuleFavIcon();

			frameHelper.showModuleMenu(true);
			frameHelper.renderModuleMenu(index);
			if (index && index.length > 1) frameHelper.setShowAllMode();
		};

		if (frameHelper.needRefreshFav)
		{
			frameHelper.getSiteMap(function () { handler(); });
			delete frameHelper.needRefreshFav;
		}
		else handler();
	},

	setActiveModuleFavIcon: function()
	{
		var ws = this.getActiveModule(), btnFav = $("#btnFavoriteWS");
		if (ws)
		{
			btnFav.removeClass("ac-rotate-90");
			ws.Favorite ? btnFav.removeClass("ac-rotate-90") : btnFav.addClass("ac-rotate-90");
			btnFav.show();
		}
		else btnFav.hide();
	},

	handleNavMenuClick: function (e)
	{
		var target = $(e.target).closest("a"), a = target[0], href;
		if (a)
		{
			href = $(a).attr("data-href");
			if (e.button == 0 || e.button === undefined)
			{
				if (href)
				{
					this.openUrl($(a).attr("data-href"), e);
					e.preventDefault();
					if (!frameHelper.isTopMenuMode()) frameHelper.needPushState = true;
				}
			}
			else
			{
				if (frameHelper.scrLocalMenu == null) frameHelper.scrLocalMenu = $("#scrLocalMenu");
				target.append(frameHelper.scrLocalMenu); target.addClass("open");
				frameHelper.scrLocalMenu.off("mousedown");
				frameHelper.scrLocalMenu.on("mousedown", frameHelper.handleLocalMenu);
			}
		}
	},

	handleLocalMenu: function (e)
	{
		var target = $(e.target).closest('LI'), a = target.closest('A');
		var href = a.attr("data-href");

		var cmd = target.attr("data-cmd");
		if (cmd) { e.stopPropagation(); a.removeClass("open"); }

		if (!href)
		{
			a.attr("open-mode", cmd);	a.trigger("click");
			return;
		}

		switch (cmd)
		{
			case "open":
				frameHelper.openUrl(a.attr("data-href"));
				break;
			case "open-newT":
				setTimeout(function () { px.openUrl(a.attr("data-href"), null, true); }, 1);
				frameHelper.hideNavMenu();
				break;
			case "open-newW":
				setTimeout(function () { px.openUrl(a.attr("data-href"), null, false, true); }, 1);
				frameHelper.hideNavMenu();
				break;
		}
	},

	setDesignButtonsState: function()
	{
		var wsSelected = this.__activeModuleID != null;
		$("#btnNewScreen").toggleClass('disabled', !wsSelected);
		$("#btnNewTile").toggleClass('disabled', !wsSelected);
	},

	//---------------------------------------------------------------------------
	startLoadAnimation: function()
	{
		$("#frameShadow").addClass("spinner");
		$("#frameShadow").show();
	},
	stopLoadAnimation: function ()
	{
		$("#frameShadow").removeClass("spinner");
		$("#frameShadow").hide();
	},

	startHelpLoadAnimation: function ()
	{
		$("#helpShadow").addClass("spinner");
		$("#helpShadow").show();
	},
	stopHelpLoadAnimation: function ()
	{
		$("#helpShadow").removeClass("spinner");
		$("#helpShadow").hide();
	},

	renderTopLinks: function (index)
	{
		if (index == null) index = this.__activeModuleID;
		var linksOwner = $("#moduleTopLinks")[0];
		var tiles = this.getTiles(this.getWs(index));
		ReactDOM.render(React.createElement(TopLinks, { items: tiles, iconRight: true }), linksOwner);
	},

	renderCategories: function ()
	{
		var owner = $("#listCategories")[0];
		ReactDOM.render(React.createElement(CategoriesList, { items: __categories }), owner);
	},

	// renders the list of workspaces
	renderModules: function (showAll)
	{
		if (showAll == null) showAll = this.isShowAllModules() || this.__designMode;

		var list = __siteMap.Workspaces, par = $("#leftMenu").get(0);
		if (!showAll)
		{
			var added = false;
			for (var i = 0; i < list.length; i++)
			{
				if (!list[i].Favorite && !added)
				{
					var ar = list.slice(0);
					ar.push({
						Title: this.getLocalString("moreItems"),
						Icon: "apps", WorkspaceID: this.__moreItemsID, Favorite: true
					});
					list = ar; added = true;
				}
				if (list[i].FakeFavorite)
				{
					var item = list[i]; list.splice(i, 1);
					list.splice(list.length, 0, item);
					break;
				}
			}
		}
		ReactDOM.render(React.createElement(MenuModules, { items: list, showAll: showAll }), par);

		setTimeout(function ()
		{
			$("#listModules div.list-group-item span.text").each(function (index, elem)
			{
				px_tm.registerOverflowTip(elem);
				var e = $(elem), tip = e.attr('title'), par = e.parent();
				if (tip)
				{
					e.removeAttr('title'); par.attr('title', tip);
					px_tm.registerElem(par[0]);
				}
			});
		}, 1);

	},

	// renders the non-favorites module's links
	renderOtherModules: function ()
	{
		var ws = __siteMap.Workspaces, list = new Array();
		for (var i = 0; i < ws.length; i++) if (!ws[i].Favorite) list.push(ws[i]);

		//list.sort(function (a, b)
		//{
		//	if (a.Title < b.Title) return -1; if (a.Title > b.Title) return 1; return 0
		//});
		ReactDOM.render(React.createElement(ModulesLinks, { items: list }), $("#moduleMenu")[0]);
	},

	isShowAllModules: function()
	{
		return $("#setupMenu li[data-cmd=full-menu]").attr("active") != null;
	},

	renderScreensToSelect: function(searchStr)
	{
		var owner = $("#listAddScreen")[0], exclude = new Array();
		var subcats = this.getSubcats(this.getActiveModule());
		if (subcats)
		{
			subcats.map(function (sc)
			{
				sc.Screens.map(function (screen) { exclude.push(screen.NodeID); });
			});
		}

		//if (!searchStr) owner.innerHTML = "";
		ReactDOM.unmountComponentAtNode(owner);
		ReactDOM.render(React.createElement(SearchResult,
			{ selectMode: true, exclude: exclude, searchString: searchStr }), owner);

		//if (!searchStr)
		//	setTimeout(function ()
		//	{
		//		$("#listAddScreen a.list-group-item").each(function (index, elem)
		//		{
		//			px_tm.registerOverflowTip(elem); px_tm.registerElem(elem);
		//		});
		//	}, 1);
	},

	//---------------------------------------------------------------------------
	// popup panel helper methods
	showPanel: function (id, params, values)
	{
		var panel = $("#" + id);
		if (panel.length == 0) return;

		panel.show();
		this.registerPopup(panel);

		if (params) panel.attr("data-params", params);
		panel.find('input:visible:first').focus();

		if (values) for (var n in values)
		{
			var ctrl = panel.find("#" + n), val = values[n], text = null;
			if (!ctrl.length) continue;

			if ($(ctrl[0]).hasClass('item-icon'))
			{
				ReactDOM.render(React.createElement(Icon,{iconName:val}),ctrl[0])
			}
			if (ctrl[0].tagName == "I")
			{
				ctrl.removeAttr("class"); ctrl.addClass("ac ac-" + val);
			}
			else
			{
				if ($.isArray(val)) { text = val[1]; val = val[0]; }
				if (text)
				{
					ctrl.attr('data-value', val);
					if (ctrl.attr('data-mode') == 'text') ctrl.val(text ? text : val)
					else ctrl.val(val + ' - ' + text);
				}
				else
				{
					ctrl.removeAttr('data-value');
					ctrl.val(val);
				}
			}
		}
		else
		{
			panel.find("input").val("");		
			panel.find('span.item-icon').each(function(i,e)
			{
				ReactDOM.unmountComponentAtNode(e);
			});
		}
	},

	// shows the confirm panel
	showConfirm: function (message, handler)
	{
		var id = "pnlConfirm", panel = $('#' + id);
		var ok = panel.find("#btnDlgOk"), cancel = panel.find("#btnDlgCancel");

		ok.off("click"); cancel.off("click");
		ok.on("click", function (e) { frameHelper.hidePanel(id); e.stopPropagation(); handler(); });
		cancel.on("click", function (e) { frameHelper.hidePanel(id); e.stopPropagation(); });

		panel.find("#lblMessage").html(message);
		panel.show();
		this.registerPopup(panel);
	},

	hidePanel: function (id)
	{
		var panel = $("#" + id);
		panel.hide(); panel.removeAttr("data-params");
		this.unregisterPopup(panel);
	},
	hideAllPanels: function ()
	{
		var panels = this.activePanels;
		if (panels)
			while (panels.length > 0)
			{
				var pnl = panels[panels.length - 1];
				if ($(pnl).hasClass("dropdown-menu")) this.hideDropDown(pnl)
				else this.hidePanel(pnl.id);
			}
	},

	hideDropDown: function (panel)
	{
		var $parent = $(panel).parents(".open");
		var $this = $parent.children('[data-toggle="dropdown"]');

		$this.attr('data-expanded', 'false');
		$parent.removeClass('open');
		this.unregisterPopup(panel);

		var h = $this.attr('data-handler');
		if (h) eval(h + '(null, false, $this)');
	},

	// registers new popup element
	registerPopup: function (panel)
	{
		var count = this.activePanels.length;
		if (count)
		{
			var last = this.activePanels[count - 1], zIndex = parseInt($(last).css('zIndex'));
			$("#bodyShadow").css('zIndex', zIndex + 1);
			panel.css('zIndex', zIndex + 2);
		}
		this.activePanels.push(panel[0]);

		$("#bodyShadow").show();
	},

	// unregisters specifed popup element
	unregisterPopup: function (panel)
	{
		var panels = this.activePanels, hasPanel = panels && panels.length;
		if (hasPanel)
		{
			panels.splice(panels.indexOf(panel[0]), 1);

			var last = panels[panels.length - 1];
			$("#bodyShadow").css('zIndex', parseInt($(last).css('zIndex')) - 1);
		}

		if (!panels || !panels.length)
		{
			$("#bodyShadow").css('zIndex', '');
			$("#bodyShadow").hide();
		}
	},
	isLastPopup: function(panel)
	{
		var panels = this.activePanels;
		return panels.length > 0 && panels[panels.length - 1] == panel[0];
	},

	registerDropDown: function (id, items, reactClass, propID, propText)
	{
		var params = { items: items }, $this = $("#" + id);
		if ($this.length == 0) return;
		if (propID) params.id = propID;
		if (propText) params.text = propText;

		var btn = $("#" + id + " + div");
		btn.off("click");
		btn.on("click", function (e)
		{
			var btn = e.target.tagName == 'DIV' ? $(e.target) : $(e.target).parent();
			var owner = btn.nextAll(".dropdown-menu");

			delete params.searchString;
			ReactDOM.render(React.createElement(reactClass, params), owner[0]);
		});

		$this.off("click");
		$this.on("input", function (e)
		{
			$(e.target).removeAttr('data-value');
			frameHelper.filterDropDown(e, reactClass, params);
		});
	},

	filterDropDown: function (e, reactClass, reactParams)
	{
		setTimeout(function ()
		{
			var input = $(e.target), searchStr = input.val();
			var owner = $(e.target).nextAll(".dropdown-menu");

			reactParams.searchString = searchStr;
			ReactDOM.render(React.createElement(reactClass, reactParams), owner[0]);

			var parent = input.parent();
			if (searchStr) parent.addClass("open"); else parent.removeClass("open");
		}, 1);
	},

	//---------------------------------------------------------------------------
	// search helper methods
	setSearchMode: function (mode)
	{
		var navBar = $('#navBarSearch');
		navBar.find('li').removeClass("pushed");
		navBar.find('li[data-cmd=' + mode + ']').addClass("pushed");
		this.__searchMode = mode;
	},

	getSearchStr: function()
	{
		return $("#searchInput").val();
	},

	setSearchStyles: function(on)
	{
		if (on)
		{
			$("#listModules").removeClass("show-active");
			$("#navMenu").addClass("search-mode");
		}
		else
		{
			$("#listModules").addClass("show-active");
			$("#navMenu").removeClass("search-mode");
			if (this.__searchH) clearTimeout(this.__searchH);
		}
		$("#navMenu").removeClass("ws-mode fav-mode empty");
	},

	performSearch: function (mode)
	{
		if (mode != null) this.setSearchMode(mode);

		var searchStr = this.getSearchStr(), menu = $('#moduleMenu');
		menu.removeClass('spinner');
		this.setSearchStyles(!!searchStr);

		if (!searchStr && this.searchAutoOpen)
		{
			this.hideNavMenu();
		}
		else
		{
			if (!this.isNavMenuVisible()) this.searchAutoOpen = true;
			if (this.isMenuConfigMode()) this.setMenuConfigMode(false);

			if (this.isTopMenuMode()) this.showNavMenu(true);
			this.showModuleMenu(true);

			if (this.__searchMode == 'menu' || !searchStr)
				this.renderModuleMenu(frameHelper.__activeModuleID, searchStr);
			else
				this.getSearchResult(0);
		}
	},

	getSearchPageSize: function(mode)
	{
		switch (mode)
		{
			case 'entity': return 10;
			case 'article': return 8;
			case 'file': return 15;
		}
	},

	getSearchResult: function (pageIndex, index)
	{
		var searchStr = this.getSearchStr();
		var menu = $('#moduleMenu'), mode = this.__searchMode;
		menu.addClass('spinner');
		ReactDOM.unmountComponentAtNode(menu[0]);

		if (index == null)
		{
			this.searchStack = new Array();
			this.searchIndex = 0;
		}
		if (index >= 0)
		{
			if (this.searchIndex != null) this.searchStack.push(this.searchIndex);
			this.searchIndex = index;
		}
		else if (index < 0)
			this.searchIndex = index = this.searchStack.pop();

		var ps = this.getSearchPageSize(mode), start = (index != null) ? index : (ps * pageIndex);
		if (start < 0) start = 0;
		delete frameHelper.hasSearchResults;

		$.getStd(this.__searchUrl + mode + '?query=' + searchStr + "&start=" + start + "&count=" + ps,
			function (data, code, xhr)
			{
				if (xhr.status != 200) return;
				menu.removeClass('spinner');
				frameHelper.hasSearchResults = data.TotalCount > 0;

				ReactDOM.render(React.createElement(EntityList, {
						data: data, pageIndex: pageIndex, pageSize: ps, mode: mode, searchString: searchStr
					}), menu[0]);
			});
	},

	screenFilter: function(screen, searchStr, exclude)
	{
		if (exclude && exclude.indexOf(screen.NodeID) >= 0) return false;

		var words = searchStr.toLowerCase().split(' ');
		for (var i = 0; i < words.length; i++)
		{
			if (screen.Title.toLowerCase().indexOf(words[i]) < 0 && 
					screen.ScreenID.toLowerCase().indexOf(words[i]) < 0) return false;
		}
		return true;
	},

	//-----------------------------------------------------------------------------
	// drag&drop methods
	getDragTarget: function (e, tagName)
	{
		var target = $(e.target);
		if (e.target.tagName != tagName) target = target.parent(tagName);
		return target;
	},
	isInsertBefore: function (e, tagName, horizontal)
	{
		var target = this.getDragTarget(e, tagName), middle;
		if (target.length == 0) return null;

		if (horizontal)
		{
			middle = target.offset().left + target.outerWidth() / 2;
			return middle > e.clientX;
		}
		middle = target.offset().top + target.outerHeight() / 2;
		return middle > e.clientY;
	},
	isDropAllowed: function (source, target, before)
	{
		if (target[0] == null || target[0] == source[0]) return false;
		if (target[0].parentNode.className != source[0].parentNode.className)
			return false;

		if (before && target.prev()[0] == source[0]) return false;
		if (!before && target.next()[0] == source[0]) return false;
		return true;
	},

	onDragLeave: function (e, tagName)
	{
		var target = frameHelper.getDragTarget(e, tagName);
		target.removeClass("dragover before after");
		e.preventDefault();
	},

	onDragOver: function (e, me, tagName, horizontal)
	{
		var target = frameHelper.getDragTarget(e, tagName);
		var before = frameHelper.isInsertBefore(e, tagName, horizontal);
		if (frameHelper.isDropAllowed(me.dragSource, target, before))
		{
			target.removeClass("dragover before after");
			target.addClass("dragover " + (before ? "before" : "after"));
		}
		e.preventDefault();
	},

	onFavoriteClick: function (e, moduleID)
	{
		var target = e.target, wsList = __siteMap.Workspaces;
		var remove = !$(target).hasClass("ac-rotate-90");
		var ws = wsList.find(function (ws) { return ws.WorkspaceID == moduleID; });
		var cmd = (remove ? "unfavorite" : "favorite") + (frameHelper.__designMode ? "Shared" : "");

		$.postStd('frameset/workspace/' + moduleID + '/' + cmd,
			function (data, code, xhr)
			{
				if (xhr.status == 204)
				{
					if (frameHelper.__activeModuleID != this.__moreItemsID) $(target).toggleClass("ac-rotate-90");
					ws.Favorite = !ws.Favorite;
					
					if (!ws.Favorite)
					{
						wsList.map(function (n) { delete n.FakeFavorite; /*n.Index = wsList.indexOf(n);*/ });
						ws.FakeFavorite = true;
					}
					else delete ws.FakeFavorite;

					frameHelper.renderModules();
					if (frameHelper.__activeModuleID == this.__moreItemsID) frameHelper.renderOtherModules();
					else frameHelper.setActiveModuleFavIcon();

					if (frameHelper.__designMode) frameHelper.reloadSiteMap = true;
				}
			}, 'json');
		e.stopPropagation();
	},

	adjustNavMenu: function ()
	{
		var bodyH = $("#bodyContent")[0].clientHeight;
		$("#navMenu").css("max-height", bodyH + "px");

		var lc = $("#leftColumn"), mw = $("#leftMenuW");
		if (frameHelper.isTopMenuMode() && lc.is(":visible"))
		{
			var delta = $("#leftFooter").outerHeight(), maxMenuH = bodyH - delta;
			if ($("#leftMenu").outerHeight() < maxMenuH)
				lc.css("height", $("#leftMenu").outerHeight() + delta + "px");
			else
				lc.css("height", maxMenuH + delta + "px");
		}
		else
		{
			lc.css("height", "");
		}
	},

	//-----------------------------------------------------------------------------
	// sets the category dialog tooltip
	setCategoryDialogTip: function (id)
	{
		var wsList = __siteMap.Workspaces, count = 0, lbl = $("#lblCatTip");
		if (id)
		{
			wsList.map(function (ws) { frameHelper.getSubcats(ws).map(function (sc) { if (sc.SubcategoryID == id) count++; }) });
			if (frameHelper.lblCatTip == null) frameHelper.lblCatTip = lbl.text();
			lbl.text(frameHelper.lblCatTip.replace('{0}', count));
		}
		lbl.toggle(id != null);
	},

	// sets the category dialog tooltip
	setScreenDialogTip: function (id)
	{
		var wsList = __siteMap.Workspaces, count = 0, lbl = $("#lblScreenTip");
		if (id)
		{
			wsList.map(function (ws) {
				frameHelper.getSubcats(ws).map(
					function (sc) { sc.Screens.map(function (s) { if (s.NodeID == id) count++; }) })
			});
			if (frameHelper.lblScreenTip == null) frameHelper.lblScreenTip = lbl.text();
			lbl.text(frameHelper.lblScreenTip.replace('{0}', count));
		}
	},

	getNextFocusIndex: function (list, active, shift, defStart)
	{
		var i = list.index(active);
		if (i >= 0)
		{
			i += shift;
			if (i < 0) i = list.length - 1; else if (i >= list.length) i = 0;
		}
		else
		{
			if (defStart != null) i = defStart + shift;
			else i = shift > 0 ? 0 : list.length - 1;
		}
		return i;
	},

	hasTopLinks: function()
	{
		var search = $("#navBarSearch"), isSearch = search.is(":visible");
		if (isSearch) return search.find("a").length > 0;
		return $("#moduleTopLinks > div").children("a").length > 0;
	},
	hasMenuLinks: function ()
	{
		return $("#moduleMenu").find("a.list-group-item").length > 0;
	},

	wsMenuVisible: function()
	{
		return $("#moduleMenu > div.workspaces").length > 0;
	},

	clearNavMenuFocus: function ()
	{
		$("#navBarMaster").find("a").removeClass("focus");
		$("#navBarDesign").find("a").removeClass("focus");
		$("#listModules").children("div").removeClass("focus");
		$("#moduleMenu > div.workspaces").children("a").removeClass("focus");
		$("#moduleTopLinks > div").children("a").removeClass("focus");
		$("#navBarSearch").find("a").removeClass("focus");
		$("#moduleMenu").find("a.list-group-item").removeClass("focus");
		delete this.focusArea;
	},

	getFocusedElement: function()
	{
		var list = $("#navBarMaster").find("a.focus:visible");
		if (list.length > 0) return $(list[0]);

		list = $("#navBarDesign").find("a.focus:visible");
		if (list.length > 0) return $(list[0]);

		list = $("#listModules").children("div.focus:visible");
		if (list.length > 0) return $(list[0]);

		list = $("#moduleTopLinks > div").children("a.focus");
		if (list.length > 0) return $(list[0]);

		list = $("#navBarSearch").find("a.focus");
		if (list.length > 0) return $(list[0]);

		list = $("#moduleMenu > div.workspaces").find("a.focus");
		if (list.length > 0) return $(list[0]);

		list = $("#moduleMenu").find("a.list-group-item.focus");
		if (list.length > 0) return $(list[0]);
	},

	//-----------------------------------------------------------------------------
	// set focus to the navigation bar
	focusNavBar: function (shift, moveAside, isTab)
	{
		if (!moveAside && shift > 0 && (!frameHelper.isTopMenuMode() || frameHelper.isNavMenuVisible()))
		{
			this.clearNavMenuFocus();
			this.focusModule(shift, false);
			return;
		}

		var par = this.__designMode ? $("#navBarDesign") : $("#navBarMaster");
		var selector = "ul.navbar-nav > li > a:visible";
		if (isTab) selector += " ,#searchInput";
		var list = par.find(selector).not(".disabled");

		var search = $("#searchInput"), ae = document.activeElement;
		var active = ae == search[0] ? search[0] :  list.filter("a.focus");
		var i = this.getNextFocusIndex(list, active, shift);

		list.removeClass("focus");
		if (list[i].tagName == "INPUT") list[i].focus(); else $(list[i]).addClass("focus");
		this.focusArea = 0;
	},

	// set focus to the module specified by shift parameter
	focusModule: function (shift, moveAside)
	{
		if ($("#bodyShadow").is(":visible")) return;

		if (moveAside && shift > 0 && frameHelper.isNavMenuVisible())
		{
			this.clearNavMenuFocus();
			if (this.wsMenuVisible()) this.focusWorkspace(1, false);
			else this.focusTopLink(shift, moveAside);
			return;
		}

		var par = $("#listModules"), list = par.children("div.list-group-item:visible");
		var active = par.children("div.focus:visible"), index = list.index(active);
		if (index == 0 && shift < 0)
		{
			this.clearNavMenuFocus();
			this.focusNavBar(1, true);
			return;
		}

		var selected = par.children("div.active:visible");
		var i = this.getNextFocusIndex(list, active, shift/*, list.index(selected)*/);

		par.children("div.list-group-item").removeClass("focus");
		par.children("div[data-id=" + $(list[i]).attr("data-id") + "]").addClass("focus");
		this.focusArea = 1;
		px.scrollToView($("#leftMenuW")[0], par.children("div.focus:visible")[0]);
	},

	focusTopLink: function (shift, moveAside)
	{
		var search = $("#navBarSearch"), isSearch = search.is(":visible");
		var par = isSearch ? search : $("#moduleTopLinks"), list = par.find("a");
		
		if ((!moveAside || list.length == 0) && (shift < 0 || this.hasMenuLinks()))
		{
			this.clearNavMenuFocus();
			if (shift > 0) this.focusMenuLink(shift, moveAside);
			else this.focusNavBar(1, true);
			return;
		}

		var active = par.find("a.focus"), index = list.index(active);
		if (index == 0 && moveAside && shift < 0)
		{
			this.clearNavMenuFocus();
			this.focusModule(1, false);
			return;
		}
		if (index == (list.length - 1) && shift > 0 && this.hasMenuLinks())
		{
			this.clearNavMenuFocus();
			this.focusMenuLink(true, false);
			return;
		}

		var i = this.getNextFocusIndex(list, active, shift);
		list.removeClass("focus");
		$(list[i]).addClass("focus");
		this.focusArea = 2;
		$("#navMenu")[0].scrollTop = 0;
	},

	//-----------------------------------------------------------------------------
	// set focus to the menu link specified by shift parameter
	focusMenuLink: function(shift, moveAside)
	{
		var par = $("#moduleMenu"), groups = par.find("div.menu-container");
		var active = par.find("a.focus"), hasActive = active.length > 0, activeGroup;

		if (hasActive) activeGroup = active.parents("div.menu-container");
		else activeGroup = $(groups[0]);
		if (activeGroup.length == 0) activeGroup = par;

		var list = activeGroup.find("a.list-group-item"), i, moveNextGroup;
		if (moveAside && hasActive)
		{
			var col = active.parents("div[data-col]");
			var cols = activeGroup.children("div[data-col]").has("div.list-group");
			if (shift < 0 && cols.index(col) == 0)
			{
				this.clearNavMenuFocus();
				this.focusModule(1, false);
				return;
			}

			i = this.getNextFocusIndex(cols, col, shift);
			if (hasActive && groups.length > 1 &&
					((shift > 0 && i == 0) || (shift < 0 && i == (cols.length - 1))))
			{
				moveNextGroup = true;
			}
			else i = list.index($(cols[i]).find("a.list-group-item")[0]);
		}
		else
		{
			if (hasActive && shift < 0 &&
				par.find("a.list-group-item").index(active) == 0 && this.hasTopLinks())
			{
				this.clearNavMenuFocus();
				this.focusTopLink(-1, true);
				return;
			}
			i = this.getNextFocusIndex(list, active, shift);
			if (hasActive && groups.length > 1 && 
					((shift > 0 && i == 0) || (shift < 0 && i == (list.length - 1))))
			{
				moveNextGroup = true;
			}
		}

		if (moveNextGroup)
		{
			i = this.getNextFocusIndex(groups, activeGroup, shift);
			list.removeClass("focus");
			list = $(groups[i]).find("a.list-group-item");
			i = (shift > 0) ? 0 : (list.length - 1);
		}

		if (list.length > 0)
		{
			list.removeClass("focus");
			$(list[i]).addClass("focus");
			this.focusArea = 3;
			px.scrollToView($("#navMenu")[0], list[i]);
		}
	},

	// set focus to the workspace list bar
	focusWorkspace: function (shift, moveAside)
	{
		var par = $("#moduleMenu > div.workspaces"), list = par.children("a");
		var active = list.filter("a.focus"), index = list.index(active);
		if (moveAside && index == 0 && shift < 0)
		{
			this.clearNavMenuFocus();
			this.focusModule(1, false);
			return;
		}

		var i = this.getNextFocusIndex(list, active, shift);
		list.removeClass("focus");
		$(list[i]).addClass("focus");
		this.focusArea = 4;
		px.scrollToView($("#navMenu")[0], list[i]);
	},
};

//=============================================================================
// ReactJS objects definition
//=============================================================================

var Icon = createReactClass({
	render: function () {
		var _icon = this.props.icon || structuredIcons[this.props.iconName];
		if (_icon) 
		{
			if (_icon.svg) 
			{
				return React.createElement("svg", { className: "svg-icon" },
					React.createElement("use", { href: __svg_icons_path + _icon.id }));
			}
			else 
			{
				return React.createElement("i", { className: 'ac ac-' + _icon.id });
			}
		}
		else 
		{
			return React.createElement("i", { className: 'ac ac-'});
		}
	}
});


// renders the modules menu
window.MenuModules = createReactClass(
{
	onClick: function (e)
	{
		var btn = e.target, role, list = __siteMap.Workspaces;

		if (frameHelper.__designMode && (role = btn.getAttribute("data-erole")) != null)
		{
			var id = $(btn.parentNode).attr('data-id'), lm = window.localizedMessages;
			var item = list.find(function (n) { return n.WorkspaceID == id });

			if (role == "remove")
			{
				frameHelper.showConfirm(frameHelper.getLocalString("confirmDeleteWorkspace", item.Title),
					function ()
					{
						$.ajaxStd({ method: 'DELETE', url: 'frameset/workspace/' + id }).
							done(function (data, code, xhr)
							{
								if (xhr.status != 204) return;
								if (item)
								{
									list.splice(list.indexOf(item), 1);
									frameHelper.reloadSiteMap = true;
								}

								if (frameHelper.__activeModuleID == id)
								{
									frameHelper.showModuleMenu(false);
									frameHelper.clearActiveModule();
								}
								frameHelper.renderModules();
							});
					});
			}
			else if (role == "edit")
			{
				var area = frameHelper.getAreaByID(item.AreaID), ad = [item.AreaID, area ? area.Name : ""];
				frameHelper.showPanel("pnlWorkspace", id,
					{ txtWsTitle: item.Title, txtWsIcon: item.Icon, imgWsIcon: item.Icon, txtWsArea: ad });
			}
			return;
		}

		if (btn.tagName != "DIV") btn = $(btn).parent("div")[0];
		var module;
		if (btn && (module = btn.getAttribute("data-id")) != null)
		{
			if (frameHelper.__activeModuleID != module || !!frameHelper.getSearchStr())
			{
				frameHelper.setActiveModule(module);
				if (e.clientX > 0) frameHelper.clearNavMenuFocus();
			}
			else if (!frameHelper.isTopMenuMode() && !frameHelper.__designMode && !frameHelper.isMenuConfigMode())
			{
				frameHelper.showModuleMenu(false);
				frameHelper.clearActiveModule();
			}
		}
	},

	onClickFav: function(e)
	{
		var id = $(e.target.parentNode).attr('data-id');
		frameHelper.onFavoriteClick(e, id);
	},

	componentDidMount: function ()
	{
		this.componentDidUpdate();
	},
	componentDidUpdate: function ()
	{
		frameHelper.adjustNavMenu();
	},

	//-----------------------------------------------------------------------------
	// drag&drop methods
	onDragStart: function (e)
	{
		this.dragSource = frameHelper.getDragTarget(e, 'DIV');
		if (e.dataTransfer && !px.IsIEorEdge) e.dataTransfer.setData('wsID', this.dragSource.attr("data-id"));
	},
	onDragOver: function (e)
	{
		frameHelper.onDragOver(e, this, 'DIV');
	},
	onDragLeave: function (e)
	{
		frameHelper.onDragLeave(e, 'DIV');
	},

	onDrop: function (e)
	{
		var target = frameHelper.getDragTarget(e, 'DIV'), before = frameHelper.isInsertBefore(e, 'DIV');
		this.onDragLeave(e);

		if (frameHelper.isDropAllowed(this.dragSource, target, before))
		{
			var prevID, nextID, query = new Array(), id = this.dragSource.attr("data-id");
			if (before)
			{
				nextID = target.attr("data-id");
				if (target.prev().length) prevID = target.prev().attr("data-id");
			}
			else
			{
				prevID = target.attr("data-id");
				if (target.next().length) nextID = target.next().attr("data-id");
			}
			if (prevID) query.push("prevWorkspaceId=" + prevID);
			if (nextID) query.push("nextWorkspaceId=" + nextID);

			var wsList = __siteMap.Workspaces, newIndex = 1;
			var ws = wsList.find(function (n) { return n.WorkspaceID == id });
			var activeWS = frameHelper.getActiveModule();

			$.postStd('frameset/workspace/' + id + '/move?' + query.join('&'),
				function (data, code, xhr)
				{
					if (xhr.status != 204 && xhr.status != 200) return;

					wsList.splice(wsList.indexOf(ws), 1);
					if (prevID)
					{
						var prev = wsList.find(function (n) { return n.WorkspaceID == prevID });
						newIndex = wsList.indexOf(prev) + 1;
					}
					wsList.splice(newIndex, 0, ws);
					frameHelper.reloadSiteMap = true;

					frameHelper.clearActiveModule();
					frameHelper.renderModules();
					if (activeWS) frameHelper.selectModule(activeWS.WorkspaceID);
				}, 'json');
		}
	},

	//-----------------------------------------------------------------------------
	// component rendering
	render: function ()
	{
		var items = this.props.items ? this.props.items : [], showAll = this.props.showAll;
		var dragStart = this.onDragStart, divAttr = {
			id: "listModules", className: "list-group modules show-active show-selected", onClick: this.onClick
		};
		var designMode = frameHelper.__designMode, onClickFav = this.onClickFav;
		if (designMode)
		{
			divAttr.onDragOver = this.onDragOver;
			divAttr.onDrop = this.onDrop;
			divAttr.onDragLeave = this.onDragLeave;
			divAttr.onDragExit = this.onDragLeave;
		}

		return React.createElement("div", divAttr,
			items.map(function (m)
			{
				if (!showAll && (!m.Favorite && !m.FakeFavorite)) return null;
				if (designMode && items.indexOf(m) == 0) return null;

				var active = m.WorkspaceID && frameHelper.__activeModuleID == m.WorkspaceID;
				var attrs = {
					className: "list-group-item" + (active ? " active" : ""),/* "data-id": m.Index ? m.Index : items.indexOf(m),*/
					key: m.WorkspaceID, "data-id": m.WorkspaceID
				};
				if (designMode) { attrs.draggable = "true"; attrs.onDragStart = dragStart; }

				var favIcon = (m.Favorite ? 'star' : 'star_border');
				return React.createElement("div", attrs,
					designMode ? React.createElement("i", { className: "ac drag-icon ac-more_vert" }) : null,
					React.createElement(Icon, { iconName: (m.Icon ? m.Icon : "file-o") }),
					//React.createElement("i", { className: "ac ac-fw ac-" + (m.Icon ? m.Icon : "file-o") }),
					React.createElement("span", { className: "text" }, m.Title),
					//(designMode || !showAll) ? null :
					//	React.createElement("i", { className: "ac ac-pull-right fav-icon ac-" + favIcon, onClick: onClickFav }),
					designMode && m.Deletable ? React.createElement("i", { className: "ac ac-close ac-fw", "data-erole": "remove" }) : null,
					designMode ? React.createElement("i", { className: "ac ac-mode_edit ac-fw", "data-erole": "edit" }) : null
				)
			}),

			items.map(function (m)
			{
				if (!showAll && (!m.Favorite && !m.FakeFavorite)) return null;
				if (designMode && items.indexOf(m) == 0) return null;

				var active = frameHelper.__activeModuleID == m.WorkspaceID;
				return React.createElement("div", {
						className: "list-group-item min" + (active ? " active" : ""), key: m.WorkspaceID + '_',
						title: m.Title/*, "data-index": m.Index ? m.Index : items.indexOf(m)*/, "data-id": m.WorkspaceID
					},
				React.createElement("i", { className: "ac ac-fw ac-" + (m.Icon ? m.Icon : "file-o") }));
			})
		);
	}
});

//-----------------------------------------------------------------------------
// renders the module top links
window.TopLinks = createReactClass(
{
	onClick: function (e)
	{
		var target = e.target, btn = $(target).closest('A');
		var id = btn.attr('data-id'), role, list = this.props.items;

		var item = list.find(function (n) { return n.TileID == id });
		var baseUrl = 'frameset/workspace/' + item.WorkspaceID + "/tiles/";

		if ((role = target.getAttribute("data-erole")) != null)
		{
			if (role == "remove")
			{
				frameHelper.showConfirm(frameHelper.getLocalString("confirmDeleteTile", item.Title),
					function ()
					{
						$.ajaxStd({ method: 'DELETE', url: baseUrl + id }).
							done(function (data, code, xhr)
							{
								if (xhr.status != 204) return;
								list.splice(list.indexOf(item), 1);
								frameHelper.renderTopLinks();
								frameHelper.reloadSiteMap = true;
							});
					});
			}
			else if (role == "edit")
			{
				var screen = frameHelper.getScreenByID(item.ScreenID), sd = [item.ScreenID, screen ? screen.Title : ""];
				frameHelper.showPanel("pnlNewTile", id, {
					txtTileTitle: item.Title, txtTileIcon: item.Icon,
					txtTileScreen: sd, txtTileParams: item.Parameters, imgTileIcon: item.Icon
				});
			}
			else if (role == "fav")
			{
				var remove = $(target).hasClass("ac-star");
				var url = baseUrl + id + '/' + (remove ? "unfavorite" : "favorite");

				$.postStd(url, function (data, code, xhr)
					{
						if (xhr.status == 204)
						{
							$(target).toggleClass("ac-star ac-star_border");
							item.Favorite = !item.Favorite;
							if (frameHelper.isFavoritesVisible())
								remove ? btn.addClass("strike-text") : btn.removeClass("strike-text");
							frameHelper.needRefreshFav = true;
						}
					}, 'json');
			}
			return;
		}

		if (!frameHelper.__designMode)
		{
			var mode = btn.attr("open-mode"), openWin = mode == "open-newW";
			var openTab = mode == null ? (e && (e.ctrlKey || e.altKey)) : mode == "open-newT";
			if (e) e.preventDefault();
			btn.removeAttr("open-mode");

			$.postStd(baseUrl + id + "/execute", function (data, code, xhr)
			{
				if (xhr.status == 200)
				{
					var url = xhr.responseJSON;
					frameHelper.openUrl(url, openTab, openWin);
				}
			});
		}
	},

	render: function ()
	{
		var items = this.props.items ? this.props.items : [], designMode = frameHelper.__designMode;
		var iconRight = this.props.iconRight, onClick = this.onClick, dragStart = this.onDragStart;

		var itemsDOM = items.map(function (m)
		{
			var favMode = frameHelper.isFavoritesVisible(), favCss = m.Favorite ? "star" : "star_border";
			var attrs = {	key: m.TileID + (favMode ? "f" : ""),
				"data-id": m.TileID, onClick: onClick, href: "javascript: void 0"
			};
			if (designMode) { attrs.draggable = "true"; attrs.onDragStart = dragStart; };

			return React.createElement("a", attrs,
				// React.createElement("i", { className: "ac ac-" + m.Icon + (iconRight ? " ac-pull-left" : "") }),
				React.createElement(Icon, { iconName:m.Icon, className: "ac " + (iconRight ? " ac-pull-left" : "") }),
				iconRight ? null : React.createElement("br", null), React.createElement("span", { className: "text" }, m.Title),
				designMode ? null : React.createElement("i", { className: "ac ac-fw ac-" + favCss, "data-erole": "fav" }),
				designMode ? React.createElement("i", { className: "ac ac-close ac-fw", "data-erole": "remove" }) : null,
				designMode ? React.createElement("i", { className: "ac ac-mode_edit ac-fw", "data-erole": "edit" }) : null
			);
		});

		var divAttr = { className: "top-links" };
		if (designMode)
		{
			divAttr.onDragOver = this.onDragOver;
			divAttr.onDrop = this.onDrop;
			divAttr.onDragLeave = this.onDragLeave;
			divAttr.onDragExit = this.onDragLeave;
		}

		return React.createElement("div", divAttr, itemsDOM);
	},

	//-----------------------------------------------------------------------------
	// drag&drop methods
	onDragStart: function (e)
	{
		this.dragSource = frameHelper.getDragTarget(e, 'A');
		if (e.dataTransfer && !px.IsIEorEdge) e.dataTransfer.setData('tileID', this.dragSource.attr("data-id"));
	},
	onDragOver: function (e)
	{
		frameHelper.onDragOver(e, this, 'A', true);
	},
	onDragLeave: function (e)
	{
		frameHelper.onDragLeave(e, 'A');
	},

	onDrop: function (e)
	{
		var target = frameHelper.getDragTarget(e, 'A'), before = frameHelper.isInsertBefore(e, 'A', true);
		this.onDragLeave(e);

		if (frameHelper.isDropAllowed(this.dragSource, target, before))
		{
			var prevID, nextID, query = new Array(), id = this.dragSource.attr("data-id");
			if (before)
			{
				nextID = target.attr("data-id");
				if (target.prev().length) prevID = target.prev().attr("data-id");
			}
			else
			{
				prevID = target.attr("data-id");
				if (target.next().length) nextID = target.next().attr("data-id");
			}
			if (prevID) query.push("prevTileId=" + prevID);
			if (nextID) query.push("nextTileId=" + nextID);

			var list = this.props.items, newIndex = 0;
			var tile = list.find(function (n) { return n.TileID == id });

			$.postStd('frameset/workspace/' + tile.WorkspaceID + '/tiles/' + id + '/move?' + query.join('&'),
				function (data, code, xhr)
				{
					if (xhr.status != 204 && xhr.status != 200) return;

					list.splice(list.indexOf(tile), 1);
					if (prevID)
					{
						var prev = list.find(function (n) { return n.TileID == prevID });
						newIndex = list.indexOf(prev) + 1;
					}
					list.splice(newIndex, 0, tile);
					frameHelper.renderTopLinks();
				}, 'json');
		}
	}
});

//-----------------------------------------------------------------------------
// one menu column
window.MenuColumn = createReactClass(
{
	onClick: function (e)
	{
		var btn = e.target, index, role;
		if (frameHelper.__designMode && (role = btn.getAttribute("data-erole")) != null)
		{
			var id = $(btn.parentNode).attr('data-id'), list = this.props.items;
			var wsID = frameHelper.__activeModuleID;

			var cat, screen;
			for (var i = 0; i < list.length; i++)
			{
				var item = list[i].Screens.find(function (n) { return n.NodeID == id });
				if (item) { screen = item; cat = list[i]; break; }
			}

			if (role == "remove")
			{
				frameHelper.showConfirm(frameHelper.getLocalString("confirmDeleteScreen", screen.Title),
					function ()
					{
						$.postJSON('frameset/workspace/' + wsID + "/remove", [id],
							function (data, code, xhr)
							{
								if (xhr.status != 200) return;
								list.map(function (i)
								{
									var item = i.Screens.find(function (n) { return n.NodeID == id });
									var screens = __siteMap.UnassignedScreens;
									if (item)
									{
										i.Screens.splice(i.Screens.indexOf(item), 1);
										screens[0].Screens.push(item);
									}
								});
								frameHelper.renderModuleMenu(wsID);
								frameHelper.reloadSiteMap = true;
							});
					});
			}
			else if (role == "edit")
			{
				var list = __categories.slice(0), compare = function (a, b)
				{
					if (a.Name < b.Name) return -1; if (a.Name > b.Name) return 1; return 0
				};
				list.sort(compare);

				var dd = $("#ddScreenCat");
				while (dd[0].options.length > 0) dd[0].remove(0);
				list.map(function (item)
				{
					dd.append(new Option(item.Name, item.SubcategoryID));
				});

				frameHelper.setScreenDialogTip(id);
				frameHelper.showPanel("pnlScreen", id, { ddScreenCat: cat.SubcategoryID, txtScreenTitle: screen.Title });
			}
			e.stopPropagation();
		}
	},

	// favorite icon click event handler
	onClickFav: function(e)
	{
		var target = e.target, list = this.props.items;
		if (target.tagName == "I" && $(target).hasClass("fav-icon"))
		{
			var a = $(target.parentNode), id = a.attr("data-id");
			var remove = $(target).hasClass("ac-star"), wsID = frameHelper.__activeModuleID;
			var url = 'frameset/screen/' + id + '/' + (remove ? "unfavorite" : "favorite") + '?workspaceId=' + wsID;

			$.postStd(url,
				function (data, code, xhr)
				{
					if (xhr.status != 204 && xhr.status != 200) return;
					
					$(target).toggleClass("ac-star ac-star_border");
					__siteMap.Workspaces.map(function (ws)
					{
						subCut = frameHelper.getSubcats(ws).map(function (sc)
						{
							var item = sc.Screens.find(function (n) { return n.NodeID == id });
							if (item) item.Favorite = !item.Favorite;
						})
					});

					frameHelper.needRefreshFav = true;
					if (frameHelper.isFavoritesVisible())
					{
						remove ? a.addClass("strike-text") : a.removeClass("strike-text");				
					}
				}, 'json');
			e.preventDefault();
		}
	},

	// pin icon click event handler
	onClickPin: function (e)
	{
		var target = $(e.target).closest('A'), icon = target.find(".fav-icon");
		if (icon)
		{
			var id = target.attr("data-id"), list = this.props.items;
			var remove = icon.hasClass("ac-check_box"), wsID = frameHelper.__activeModuleID;
			var cmd = (remove ? "unpin" : "pin") + (frameHelper.__designMode ? "Shared" : "");
			var url = 'frameset/screen/' + id + '/' + cmd + '?workspaceId=' + wsID;

			$.postStd(url,
				function (data, code, xhr)
				{
					if (xhr.status != 204 && xhr.status != 200) return;

					icon.toggleClass("ac-check_box ac-check_box_outline_blank");
					list.map(function (i)
					{
						var item = i.Screens.find(function (n) { return n.NodeID == id });
						if (item) item.Pinned = !item.Pinned;
					});
					if (frameHelper.__designMode) frameHelper.reloadSiteMap = true;
				}, 'json');
			e.preventDefault();
		}
	},

	onClickSel: function (e)
	{
		var target = e.target.tagName == 'A' ? e.target : e.target.parentNode;
		var icon = $(target).find(".fav-icon");
		if (icon)
		{
			var id = $(target).attr("data-id"), list = frameHelper.selectedScreens;
			icon.toggleClass("ac-check_box ac-check_box_outline_blank");

			if (icon.hasClass("ac-check_box")) list.push(id);
			else list.splice(list.indexOf(id), 1);
		}
	},

	//---------------------------------------------------------------------------
	render: function ()
	{
		var items = this.props.items, searchRE, replacer;
		var showFav = this.props.showFav, searchString = this.props.searchString;
		var selectMode = this.props.selectMode, showPin = this.props.showPin;
		if (searchString)
		{
			searchString = searchString.trim().toLowerCase();
			var exp = searchString.replace(/\ /g, '|');
			exp = exp.replace("+", "\\+").replace("?", "\\?").replace("*", "\\*");
			exp = exp.replace("\\", "\\\\").replace("(", "\\(").replace(")", "\\)");

			searchRE = new RegExp(exp, 'ig');
			replacer = function (str, p1, p2, offset, s) { return '<span class="search-mark">' + str + '</span>'; };
		}

		var onClick = this.onClick, onDragStart = this.props.onDragStart;
		var onClickFav = this.onClickFav, onClickSel = this.onClickSel, onClickPin = this.onClickPin;

		return React.createElement("div", null, items.map(function (l)
		{
			var screens = l.Screens, onlyPin = frameHelper.isShowOnlyPin();
			var renderDesignIcons = (frameHelper.__designMode && !selectMode);
			var renderRemoveIcon = function (handler)
			{
				return renderDesignIcons ? React.createElement("i", {
					className: "ac ac-close ac-fw", "data-erole": "remove", onClick: handler }) : null;
			}
			var renderEditIcon = function (handler)
			{
				return renderDesignIcons ? React.createElement("i", {
					className: "ac ac-mode_edit ac-fw", "data-erole": "edit", onClick: handler }) : null
			}

			if (onlyPin && !searchString)
			{
				var screens2 = new Array();
				for (var i = 0; i < screens.length; i++)
					if (screens[i].Pinned) screens2.push(screens[i]);
				screens = screens2;
			}

			var props = { className: "list-group screens" + (renderDesignIcons ? " design" : ""), key: items.indexOf(l) };
			if (renderDesignIcons && l.SubcategoryID) props["data-id"] = l.SubcategoryID;

			return screens.length == 0 ? null : React.createElement("div", props,
				React.createElement("h3", null,
					l.Icon ? React.createElement("i", { className: "ac ac-fw ac-" + l.Icon }) : null,
					l.Name.replace("&nbsp;", "\u00a0")
				),
				screens.map(function (s)
				{
					var url = s.Url, active = false, activeUrl = window.__activeUrl;
					var screen = frameHelper.getScreenUrl(url), listUrl = window.__activeListUrl;
					if (activeUrl && screen.indexOf(activeUrl) >= 0) active = true;
					if (listUrl && screen.indexOf(listUrl) >= 0) active = true;

					var renderFav = (selectMode || showPin || showFav)  && !!s.Title.trim();
					var favIcon = selectMode ? "ac-check_box_outline_blank" : (s.Favorite ? "ac-star" : "ac-star_border");
					if (showPin) favIcon = s.Pinned ? "ac-check_box" : "ac-check_box_outline_blank";

					var favMode = !selectMode && frameHelper.isFavoritesVisible();
					var linkClickH = showPin ? onClickPin : (selectMode ? onClickSel : null);
					var attrs = {
						className: "list-group-item" + (active ? " active" : ""), "data-href": url,
						key: s.NodeID + (favMode ? "f" : ""), "data-id": s.NodeID, onClick: linkClickH, href: "javascript: void 0"
					};
					if (renderDesignIcons) { attrs.draggable = "true"; attrs.onDragStart = onDragStart; }

					var text = s.Title;
					if (searchRE) text = text.replace(searchRE, replacer);
					if (selectMode || searchString) attrs.title = (s.ScreenID + ' - ') + s.Title;

					return React.createElement("a", attrs,
						renderDesignIcons ? React.createElement("i", { className: "ac drag-icon ac-more_vert" }) : null,
						React.createElement("i", {
							className: "ac fav-icon " + favIcon, onClick: (selectMode || showPin) ? null : onClickFav,
							style: renderFav ? null : { visibility: "hidden" }
						}),
						React.createElement("span", { className: "text", dangerouslySetInnerHTML: { __html: text } }),
						renderRemoveIcon(onClick), renderEditIcon(onClick)
					);
				}
				));
		}));
	}
});

//-----------------------------------------------------------------------------
// all menu columns
window.ModuleMenu = createReactClass(
{
	render: function ()
	{
		var searchStr = this.props.searchString, showFav = this.props.showFav;
		var index = this.props.index, items = this.props.items, list, screens;
		var showPin = this.props.showPin;
		if (items == null) items = frameHelper.getSubcats(frameHelper.getWs(index));

		// filter by search string
		if (searchStr)
		{
			list = new Array();
			for (var j = 0; j < items.length; j++)
			{
				screens = new Array(); l = items[j];
				for (var i = 0; i < l.Screens.length; i++)
				{
					if (frameHelper.screenFilter(l.Screens[i], searchStr, this.props.exclude))
						screens.push(l.Screens[i]);
				}
				if (screens.length > 0)
					list.push({ Name: l.Name, Icon: l.Icon, Column: l.Column, Screens: screens });
			}
			items = list;
		}

		var columns = new Array();
		for (var j = 0; j < 100; j++) columns.push(j);

		if (this.props.selectMode)
		{
			// here we split too long sub-categories
			list = new Array();
			for (var j = 0; j < items.length; j++)
			{
				var l = items[j], sc;
				if (l.Screens.length > 20)
				{
					for (var i = 0; i < l.Screens.length; i += 20)
					{
						list.push({
							SubcategoryID: l.SubcategoryID,
							Name: l.Name, Icon: l.Icon, Column: l.Column, Screens: l.Screens.slice(i, i + 20)
						});
					}
				}
				else list.push(l);
			}
			items = list;
		}

		// here we will try to adjust links layout automatically
		if (this.props.width)
		{
			items = this.arrangeLinks2(items, true);
		}
		else
		{
			var count = 0, col = 0;
			for (var i = 0; i < items.length; i++)
			{
				if (items[i].Column != null && items[i].Column > col)
					col = items[i].Column;
			}
			if (col > 0) col++;

			for (var i = 0; i < items.length; i++)
			{
				if (items[i].Column != null) continue;
				count += items[i].Screens.length;
				if (count >= 20 && i > 0) { count = items[i].Screens.length; col++; }
				items[i].Column = col;
			}
		}

		var selectMode = this.props.selectMode;
		var onDragStart = this.onDragStart;
		var colsDOM = columns.map(function (i)
		{
			var list = items.filter(function (l) { return l.Column == i; });
			return (list && list.length > 0) ?
				React.createElement("div", { "data-col": i }, React.createElement(MenuColumn,	{
						items: list, searchString: searchStr, showFav: showFav, 
						showPin: showPin, selectMode: selectMode, onDragStart: onDragStart
				})) : null;
		});

		var divAttr = { className: "menu-container" };
		if (frameHelper.__designMode && !selectMode)
		{
			divAttr.onDragOver = this.onDragOver;
			divAttr.onDrop = this.onDrop;
			divAttr.onDragLeave = this.onDragLeave;
			divAttr.onDragExit = this.onDragLeave;
		}
		return React.createElement("div",
			divAttr, colsDOM, React.createElement("div", { className: "clear-float" }));
	},

	//---------------------------------------------------------------------------
	// calculates the expected count of links in one column
	getItemsInCol: function (items, ignoreFav)
	{
		var count = 0, onlyFav = !ignoreFav && frameHelper.isShowOnlyPin()
		for (var j = 0; j < items.length; j++)
		{
			if (onlyFav)
			{
				for (var i = 0; i < items[j].Screens.length; i++)
					if (items[j].Screens[i].Pinned) count++;
			}
			else count += items[j].Screens.length;
		}

		var cols = (this.props.width > 300) ? Math.floor(this.props.width / 300) : 1;
		return [cols, Math.ceil(count / cols)];
	},

	// arrange links
	arrangeLinks: function (items)
	{
		var itemsInCol = this.getItemsInCol(items)[1], list = new Array();
		var count = 0, onlyFav = frameHelper.isShowOnlyPin(), col = 0;
		for (var j = 0; j < items.length; j++)
		{
			var l = items[j], screens = l.Screens;
			if (onlyFav) screens = screens.filter(function (l) { return l.Pinned; });
			if (screens.length == 0) continue;

			var sc = { SubcategoryID: l.SubcategoryID, Name: l.Name, Icon: l.Icon, Column: col, Screens: new Array() };
			list.push(sc);

			for (var i = 0; i < screens.length; i++)
			{
				sc.Screens.push(screens[i]);
				count++;
				if (count == itemsInCol)
				{
					count = 0; col++;
					if (i < (screens.length - 1))
					{
						sc = { SubcategoryID: l.SubcategoryID, Name: l.Name, Icon: l.Icon, Column: col, Screens: new Array() };
						list.push(sc);
					}
				}
			}
		}
		return list;
	},

	// arrange links 2
	arrangeLinks2: function (items, ignoreFav)
	{
		var pair = this.getItemsInCol(items, ignoreFav), cols = pair[0], itemsInCol = pair[1];
		var count = 0, onlyFav = !ignoreFav && frameHelper.isShowOnlyPin(), col = 0, list;
		do
		{
			list = new Array(); count = col = 0; 
			for (var j = 0; j < items.length; j++)
			{
				var l = items[j], screens = l.Screens;
				if (onlyFav) screens = screens.filter(function (l) { return l.Pinned; });
				if (screens.length == 0) continue;

				if ((screens.length + count) > itemsInCol)
				{
					if (count > 0) col++;
					count = 0;
				}

				var sc = { SubcategoryID: l.SubcategoryID, Name: l.Name, Icon: l.Icon, Column: col, Screens: screens };
				list.push(sc);
				count += screens.length;
			}
			itemsInCol++;
		}
		while (col >= cols);

		return list;
	},

	//---------------------------------------------------------------------------
	// drag&drop methods
	onDragStart: function (e)
	{
		this.dragSource = frameHelper.getDragTarget(e, 'A');
		if (e.dataTransfer && !px.IsIEorEdge) e.dataTransfer.setData('dragID', this.dragSource.attr("data-id"));
	},

	onDragOver: function (e)
	{
		frameHelper.onDragOver(e, this, 'A');
	},

	onDragLeave: function (e, tagName)
	{
		frameHelper.onDragLeave(e, 'A');
	},

	onDrop: function (e)
	{
		var target = frameHelper.getDragTarget(e, 'A'), before = frameHelper.isInsertBefore(e, 'A');
		this.onDragLeave(e);

		if (frameHelper.isDropAllowed(this.dragSource, target, before))
		{
			var prevID, nextID, query = new Array(), id = this.dragSource.attr("data-id");
			if (before)
			{
				nextID = target.attr("data-id");
				if (target.prev().length) prevID = target.prev().attr("data-id");
			}
			else
			{
				prevID = target.attr("data-id");
				if (target.next().length) nextID = target.next().attr("data-id");
			}
			if (prevID == null && nextID == null) return;
			query.push("workspaceId=" + frameHelper.__activeModuleID);
			if (prevID) query.push("prevNodeId=" + prevID);
			if (nextID) query.push("nextNodeId=" + nextID);

			var url = 'frameset/screen/' + id;
			var oldCategory = frameHelper.getScreenInActiveWS(id, true)[1];
			var newCategory = frameHelper.getScreenInActiveWS(prevID || nextID, true)[1];
			var anySuccess = 0, count = 1;
			var success = function ()
			{
				px.stopWC();
				if (anySuccess == count)
					frameHelper.getSiteMap(function ()
					{
						frameHelper.renderModuleMenu(frameHelper.__activeModuleID);
						frameHelper.reloadSiteMap = true;
					}, true);
			};

			px.startWC();
			var batch = $.batch();
			if (newCategory.SubcategoryID != oldCategory.SubcategoryID)
			{
				count += 1;
				batch.add(function()
				{
					$.postStd(url + '/changeSubcategory?subcategoryId=' + newCategory.SubcategoryID,
						function(data, code, xhr)
						{
							if (xhr.status == 204) { anySuccess++; success(); }
						});
				});
			}
			batch.add(function()
			{
				$.postStd(url + '/move?' + query.join('&'),
					function(data, code, xhr)
					{
						if (xhr.status == 204) { anySuccess++; success(); }
					}, 'json');
			});
			batch.send();
		}
	}
});

//-----------------------------------------------------------------------------
// search panel
window.SearchResult = createReactClass(
{
	isScreenInFilter: function (filter, categories, countItems)
	{
		if (!filter) return true;
		filter = filter.toLowerCase();

		var count = 0;
		for (var j = 0; j < categories.length; j++)
		{
			var items = categories[j].Screens;
			for (var k = 0; k < items.length; k++)
			{
				if (frameHelper.screenFilter(items[k], filter, this.props.exclude))
				{
					count++;
					if (!countItems) return count;
				}
			}
		}
		return count;
	},

	render: function ()
	{
		var items = __siteMap.Workspaces.slice(1), props = this.props, exclude = props.exclude;
		var filter = this.isScreenInFilter, selectMode = props.selectMode, me = this;

		var count, searchStr = props.searchString;
		if (selectMode)
		{
			var otherWs = new Object(); otherWs.Title = frameHelper.getLocalString("other");
			otherWs.Subcategories = __siteMap.UnassignedScreens.slice(0);
			selectMode ? items.splice(0, 0, otherWs) : items.push(otherWs);
		}

		this.total = 0;
		items.map(function (m)
		{
			if (m.Subcategories && m.Subcategories.length > 0)
				me.total += filter(searchStr, m.Subcategories, true);
		});

		if (this.total == 0)
			return React.createElement(SearchNotFound, { text: searchStr, selectMode: selectMode });

		return React.createElement("div", null,
			React.createElement("div", null,
				items.map(function (m)
				{
					if (!m.Subcategories || m.Subcategories.length == 0 || !filter(searchStr, m.Subcategories))
						return null;

					return [React.createElement("div", { className: "module-name" },
						React.createElement("i", { className: "ac ac-" + (m.Icon ? m.Icon : "file-o") }),
						React.createElement("span", { className: "text" }, m.Title)),
						React.createElement(ModuleMenu, { items: m.Subcategories, exclude: exclude,
							searchString: searchStr, selectMode: selectMode, width: $("#rightColumn").width() - 130
						})];
				})
		));
	},

	componentDidMount: function ()
	{
		this.componentDidUpdate();
	},
	componentDidUpdate: function ()
	{
		if (!this.props.selectMode)
			$("#searchItemsCount").html('(' + this.total + ')');
	}
});

window.SearchNotFound = createReactClass(
{
	render: function ()
	{
		var selectMode = this.props.selectMode;
		return React.createElement("div", { className: "search-failed" },
			React.createElement("div", null, frameHelper.getLocalString("searchWarn1"),
				React.createElement("span", { className: "text" }, this.props.text + '.')),
			React.createElement("br"), 
			React.createElement("div", null, frameHelper.getLocalString("searchWarn2")),
			React.createElement("ul", null,
				React.createElement("li", null, frameHelper.getLocalString("searchWarn3")),
				React.createElement("li", null, frameHelper.getLocalString("searchWarn4")),
				React.createElement("li", null, frameHelper.getLocalString("searchWarn5")),
				selectMode ? React.createElement("li", null, frameHelper.getLocalString("searchWarn6"), React.createElement("br"),
					frameHelper.getLocalString("searchWarn7")) : null
			)
		);
	}
});

//-----------------------------------------------------------------------------
// icons list class
window.IconsList = createReactClass(
{
	render: function ()
	{
		var items = this.props.items, searchStr = this.props.searchString;
		if (searchStr)
		{
			
			if (searchStr) searchStr = searchStr.trim().toLowerCase();
			items = this.props.items.filter(function (m)
			{			
				return m.title.toLowerCase().indexOf(searchStr) >= 0;
			});
		}

		return React.createElement("ul", { className: "", id: "imagesMenu" },
			items.map(function (m)
			{
				
				var iconName = m.title;
				return React.createElement("li", { key: m.id, "data-value": m.lookup },
					// React.createElement("i", { className: ("ac ac-" + m) }),
					React.createElement(Icon, { icon: m }),
					React.createElement("a", null, iconName.replace(/_/g, ' ')));
			}));
	}
});

// strings list class
window.StringList = createReactClass(
{
	render: function ()
	{
		var items = this.props.items, searchStr = this.props.searchString;
		if (searchStr)
		{
			if (searchStr) searchStr = searchStr.trim().toLowerCase();
			items = new Array();
			this.props.items.map(function (m)
			{
				if (m.toLowerCase().indexOf(searchStr) >= 0) items.push(m);
			});
		}

		return React.createElement("ul", null,
			items.map(function (m)
			{
				return React.createElement("li", { key: items.indexOf(m), "data-value": m }, React.createElement("a", null, m));
			}));
	}
});

// items list class
window.ItemsList = createReactClass(
{
	render: function ()
	{
		var items = this.props.items, searchStr = this.props.searchString;
		var id = this.props.id, text = this.props.text;
		if (searchStr)
		{
			if (searchStr) searchStr = searchStr.trim().toLowerCase();
			items = new Array();
			this.props.items.map(function (m)
			{
				if (m[text].toLowerCase().indexOf(searchStr) >= 0) items.push(m);
			});
		}

		return React.createElement("ul", null,
			items.map(function (m)
			{
				return React.createElement("li", { key: items.indexOf(m), "data-value": m[id], "data-text": m[text] },
					React.createElement("a", null, m[text]));
			}));
	}
});

// screens list class
window.ScreensList = createReactClass(
{
	render: function ()
	{
		var items = __siteMap.ScreensAvailableForTiles.slice(0);
		var searchStr = this.props.searchString;

		if (searchStr)
		{
			if (searchStr) searchStr = searchStr.trim().toLowerCase();
			var screens = new Array();
			items.map(function (m)
			{
				if (m.Title.toLowerCase().indexOf(searchStr) >= 0 ||
						m.ScreenID.toLowerCase().indexOf(searchStr) >= 0)
				{
					screens.push(m);
				}
			});
			items = screens;
		}

		return React.createElement("ul", { className: "", id: "screensMenu" },
			items.map(function (m)
			{
				return React.createElement("li", { key: m.NodeID, "data-value": m.ScreenID, "data-text": m.Title },
					React.createElement("a", null, m.ScreenID + ' - ' + m.Title));
			}));
	}
});

//-----------------------------------------------------------------------------
// categories panel
window.CategoriesList = createReactClass(
{
	onClickCat: function (e)
	{
		var btn = e.target, index, role;
		if (frameHelper.__designMode && (role = btn.getAttribute("data-erole")) != null)
		{
			var id = $(btn.parentNode).attr('data-id');
			var sc = __categories.find(function (cat) { return cat.SubcategoryID == id });

			if (role == "remove")
			{
				frameHelper.showConfirm(frameHelper.getLocalString("confirmDeleteCategory", sc.Name),
					function ()
					{
						$.ajaxStd({ method: 'DELETE', url: 'frameset/subcategory/' + id }).
							done(function (data, code, xhr)
							{
								if (xhr.status == 204)
								{
									__categories.splice(__categories.indexOf(sc), 1);
									frameHelper.renderCategories();
									frameHelper.reloadSiteMap = true;
								}
							});
					});
			}
			else if (role == "edit")
			{
				frameHelper.setCategoryDialogTip(id);
				frameHelper.showPanel("pnlCategory",
					id, { txtCatIcon: sc.Icon, txtCatTitle: sc.Name, imgCatIcon: sc.Icon });
			}
			e.stopPropagation();
		}
	},

	render: function ()
	{
		var items = this.props.items, onClickCat = this.onClickCat, dragStart = this.onDragStart;
		var renderDesignIcons = (frameHelper.__designMode);
		var divAttr = { className: "list-group screens" };
		if (frameHelper.__designMode)
		{
			divAttr.onDragOver = this.onDragOver;
			divAttr.onDrop = this.onDrop;
			divAttr.onDragLeave = this.onDragLeave;
			divAttr.onDragExit = this.onDragLeave;
		}

		return React.createElement("div", divAttr,
			items.map(function (cat)
			{
				if (!cat.SubcategoryID || cat.SubcategoryID.indexOf('-aaaa-') > 0)
					return null;

				var attrs = {
					key: cat.SubcategoryID, className: "list-group-item", "data-id": cat.SubcategoryID
				};
				if (frameHelper.__designMode) { attrs.draggable = "true"; attrs.onDragStart = dragStart; }

				return React.createElement("div", attrs,
					renderDesignIcons ? React.createElement("i", { className: "ac drag-icon ac-more_vert" }) : null,
					cat.Icon ? React.createElement("i", { className: ("ac ac-fw ac-" + cat.Icon) }) : null,
					React.createElement("span", { className: "text" }, cat.Name),
					cat.Deletable ? React.createElement("i", { className: "ac ac-close ac-fw", "data-erole": "remove", onClick: onClickCat }) : null,
					React.createElement("i", { className: "ac ac-mode_edit ac-fw", "data-erole": "edit", onClick: onClickCat })
				);
			}));
	},

	//-----------------------------------------------------------------------------
	// drag&drop methods
	onDragStart: function (e)
	{
		this.dragSource = frameHelper.getDragTarget(e, 'DIV');
		if (e.dataTransfer && !px.IsIEorEdge) e.dataTransfer.setData('catID', this.dragSource.attr("data-id"));
	},

	onDragOver: function (e)
	{
		frameHelper.onDragOver(e, this, 'DIV');
	},

	onDragLeave: function (e)
	{
		frameHelper.onDragLeave(e, 'DIV');
	},

	onDrop: function (e)
	{
		var target = frameHelper.getDragTarget(e, 'DIV'), before = frameHelper.isInsertBefore(e, 'DIV');
		this.onDragLeave(e);

		if (frameHelper.isDropAllowed(this.dragSource, target, before))
		{
			var prevID, nextID, query = new Array(), id = this.dragSource.attr("data-id");
			if (before)
			{
				nextID = target.attr("data-id");
				if (target.prev().length) prevID = target.prev().attr("data-id");
			}
			else
			{
				prevID = target.attr("data-id");
				if (target.next().length) nextID = target.next().attr("data-id");
			}
			if (prevID) query.push("prevSubcategoryId=" + prevID);
			if (nextID) query.push("nextSubcategoryId=" + nextID);

			var list = __categories, newIndex = 0;
			var ws = list.find(function (n) { return n.SubcategoryID == id });

			$.postStd('frameset/subcategory/' + id + '/move?' + query.join('&'),
				function (data, code, xhr)
				{
					if (xhr.status != 204 && xhr.status != 200) return;

					list.splice(list.indexOf(ws), 1);
					if (prevID)
					{
						var prev = list.find(function (n) { return n.SubcategoryID == prevID });
						newIndex = list.indexOf(prev) + 1;
					}
					list.splice(newIndex, 0, ws);
					frameHelper.renderCategories();
				}, 'json');
		}
	}
});

//-----------------------------------------------------------------------------
// entity panel
window.EntityList = createReactClass(
{
	onNext: function (e)
	{
		var index = $(e.target).closest('DIV').attr('data-index');
		frameHelper.getSearchResult(this.props.pageIndex + 1, index);
	},
	onPrev: function (e)
	{
		frameHelper.getSearchResult(this.props.pageIndex - 1, -1);
	},

	onClick: function (e)
	{
		var btn = $(e.target).closest('A'), id = btn.attr('data-id');
		var alt = e && (e.ctrlKey || e.altKey);
		var mode = btn.attr("open-mode"), openWin = mode == "open-newW";
		var openTab = mode == null ? alt : mode == "open-newT";

		if (e) e.preventDefault();
		btn.removeAttr("open-mode");

		if (frameHelper.__searchMode == 'article')
		{
			if (alt) frameHelper.showHelpArticleByID(id);
			else px.openFrameset("about:blank", null, "wikiPage=" + id);
			return;
		}

		$.postJSON(frameHelper.__searchUrl + "/" + frameHelper.__searchMode + "/navigate", id,
			function (data, code, xhr)
			{
				if (xhr.status == 200)
				{
					var url = xhr.responseJSON;
					frameHelper.openUrl(url, openTab, false);
				}
			});
	},

	render: function ()
	{
		var data = this.props.data, items = data.Results, onClick = this.onClick;
		var nextCss = (data.HasNextPage ? " active" : "");
		var prevCss = (data.HasPreviousPage ? " active" : "");
		var prevIndex = data.PreviousIndex, nextIndex = data.NextIndex;

		if (items.length == 0)
			return React.createElement(SearchNotFound, { text: this.props.searchString });

		return React.createElement("div", { className: "list-group entity" },
			items.map(function (item)
			{
				var attrs = {
					key: item.ID, className: "list-group-item", "data-id": item.ID, onClick: onClick
				};

				return React.createElement("a", attrs,
					React.createElement("div", { className: "title", dangerouslySetInnerHTML: { __html: item.Title }}),
					item.Line1 ? React.createElement("div", { className: "text", dangerouslySetInnerHTML: { __html: item.Line1 } }) : null,
					item.Line1 ? React.createElement("div", { className: "text", dangerouslySetInnerHTML: { __html: item.Line2 }}) : null,
					item.Text ? React.createElement("div", { className: "text", dangerouslySetInnerHTML: { __html: item.Text } }) : null
				);
			}),
			React.createElement("div", { className: "pager" },
				React.createElement("div", { className: 'arrow' + nextCss, onClick:
						nextCss ? this.onNext : null, 'data-index': nextIndex != null ? nextIndex : null
					},
					'NEXT', React.createElement("i", { className: "ac ac-keyboard_arrow_right" })
				),
				React.createElement("div", { className: 'arrow' + prevCss, onClick:
						prevCss ? this.onPrev : null, 'data-index': prevIndex != null ? prevIndex : null
					},
					React.createElement("i", { className: "ac ac-keyboard_arrow_left" }), 'PREV'
				)
			));
	}
});

//-----------------------------------------------------------------------------
// links for non-favorite modules
window.ModulesLinks = createReactClass(
{
	onClick: function(e)
	{
		var btn = e.target.tagName != 'A' ? e.target.parentNode : e.target;
		var id = $(btn).attr("data-id"), wsList = __siteMap.Workspaces;
		var ws = wsList.find(function (n) { return n.WorkspaceID == id });
		if (ws)
		{
			wsList.map(function (n) { delete n.FakeFavorite; /*n.Index = wsList.indexOf(n);*/ });
			ws.FakeFavorite = true;
			frameHelper.renderModules(false);
			setTimeout(function ()
			{
				frameHelper.setActiveModule(ws.WorkspaceID);
				if (!e.clientX) frameHelper.focusModule(-1, false);
			}, 0);
		}
	},

	onClickRole: function (e)
	{
		var btn = e.target, index, role;
		if ((role = btn.getAttribute("data-erole")) != null)
		{
			var id = $(btn.parentNode).attr('data-id');
			if (role == "pin") frameHelper.onFavoriteClick(e, id);
			e.stopPropagation();
		}
	},

	render: function ()
	{
		var areas = __siteMap.Areas, list = new Array();
		var items = this.props.items, onClick = this.onClick, onClickRole = this.onClickRole;

		items.map(function (m)
		{
			var area;
			if ((area = list.find(function (n) { return n.AreaID == m.AreaID; })) == null)
			{
				var a = areas.find(function (a) { return a.AreaID == m.AreaID; });
				if (a != null)
				{
					area = { AreaID: m.AreaID, Name: a.Name, Modules: new Array(), Order: areas.indexOf(a) };
					list.push(area);
				}
			}
			if (area) area.Modules.push(m);
		});

		list.sort(function (a, b)
		{
			if (a.Order < b.Order) return -1; if (a.Order > b.Order) return 1; return 0
		});

		return React.createElement("div", { className: "workspaces", onClick: onClick },
		list.map(function(a)
		{
			return [React.createElement("div", { className: "module-name" }, a.Name),
				a.Modules.map(function (m)
				{
					var url = "javascript: void 0";
					return React.createElement("a", { href: url, "data-id": m.WorkspaceID },
						React.createElement("i", { className: "ac ac-" + m.Icon }), React.createElement("br", null), m.Title,
						React.createElement("i", { className: "ac ac-pin ac-rotate-90", "data-erole": "pin", onClick: onClickRole }));
				})
			]
		}));
	}
});

//-----------------------------------------------------------------------------
// wiki links
window.WikiLinks = createReactClass(
{
	onClick: function (e)
	{
		var link = $(e.target).closest('A'), wiki, data = frameHelper.screenHelpData;
		if ((wiki = link.attr("data-id")) != null)
		{
			if (data == null) data = new Object();
			data.Wiki = wiki;
			data.Url = link.attr("data-url");
			data.Article = px.getQueryParamVal("pageID", data.Url);
			data.WikiName = frameHelper.getWikiName(wiki);
			delete data.articleOpened;

			frameHelper.hideWikiFrame(false);
			frameHelper.showHelpArticle(data);
		}
	},

	render: function ()
	{
		var list = this.props.items, onClick = this.onClick;
		var url = "javascript: void 0";

		return React.createElement("div", { className: "wikies", onClick: onClick },
			list.map(function (w)
			{
				var descr = w.Description ? w.Description : "Some text explaining the content of the article.";
				return React.createElement("div", null,
					React.createElement("span", { className: "title" }, w.Title), React.createElement("br", null),
					descr ? React.createElement("span", { className: "descr" }, descr) : null,
					descr ? React.createElement("br", null) : null,
					React.createElement("a", { href: url, 'data-id': w.PageID, 'data-url': w.DefaultUrl },
						frameHelper.getLocalString("explore"))
				);
			}));
	}
});

//=============================================================================
// DropDown menu support
//=============================================================================

(function ($) 
{
	var toggle = '[data-toggle="dropdown"]'
	var Dropdown = function (element)
	{
		$(element).on('click.px.dropdown', this.toggle)
	}
	
	function callHandler($this, e)
	{
		var h = $this.attr('data-handler');
		if (h)
		{
			var $isOpen = $this.parent().hasClass("open");
			eval(h + '(e, $isOpen, $this)');
		}
	}

	function callHandlerV($this, val)
	{
		var h = $this.attr('data-handler');
		if (h) eval(h + '(val)');
	}

	function isCloseButton(btn)
	{
		var li = $(btn).closest('li');
		if (li.length && li.attr('data-close')) return true;

		var btn = $(btn).closest('button');
		return btn.length && btn.attr('data-close');
	}

	function clearMenus(e, activeDD)
	{
		if (e && e.which === 3) return;
		//$("body").css("pointer-events", "");

		var activePopup = frameHelper.activePopup;
		$(toggle).each(function ()
		{
			var $this = $(this), $parent = $this.parent();
			if (!$parent.hasClass('open')) return;

			if (e && e.type == 'click' && /input|textarea/i.test(e.target.tagName) && $.contains($parent[0], e.target))
				return;

			if (e && e.isDefaultPrevented()) return;
			if (activeDD && $.contains($parent[0], activeDD)) return;
			if (activePopup && $.contains(activePopup, e.target)) return;

			if ($this.attr('data-shadow') != null)
			{
				var menu = $parent.children(".dropdown-menu");
				if (menu.is(":visible"))
				{
					if ((e && $.contains(menu[0], e.target) && !isCloseButton(e.target)) || !frameHelper.isLastPopup(menu))
						return;
					frameHelper.unregisterPopup(menu);
				}
			}

			$this.attr('data-expanded', 'false');
			$parent.removeClass('open');
			callHandler($this, e);
		});
	}
	window.clearDropDownMenus = clearMenus;

	Dropdown.prototype.toggle = function (e)
	{
		var $this = $(this);
		if ($this.is('.disabled, :disabled')) return;

		var $parent = $this.parent(), isActive = $parent.hasClass('open');
		clearMenus(e, $parent[0]);

		if (!isActive)
		{
			if (e.isDefaultPrevented()) return;

			var menu = $parent.children(".dropdown-menu");
			if (menu.length > 0 && $this.attr('data-shadow') != null)
			{
				frameHelper.hideAllPanels();
				frameHelper.registerPopup(menu);
			}

			$this.trigger('focus').attr('data-expanded', 'true');
			$parent.toggleClass('open');
			callHandler($this, e);
		}
		return false;
	}

	Dropdown.prototype.menuClick = function (e)
	{
		var $this = $(this), val;
		if ($this.is('.disabled, :disabled')) return;

		if ($this.attr('role') == 'checkbox')
		{
			var group = $this.attr('group');
			if (!$this.attr('active'))
			{
				if (group) $this.parent().children('li[group=' + group + ']').removeAttr('active');
				$this.attr('active', 'active');
			}
			else if(!group) $this.removeAttr('active');
		}
		else if ((val = $this.attr('data-value')) != null)
		{
			var menu = $this.parents('.dropdown-menu')[0];
			if (menu)
			{
				var target = $(menu).attr('data-target');
				if (target)
				{
					var text = $this.attr('data-text'), displayText = text ? (val + ' - ' + text) : val;
					target = $('#' + target);
					if (target.attr('data-mode') == 'text') displayText = text ? text : val;

					var oldVal = target.attr('data-value') != null ? target.attr('data-value') : target.val();
					if (oldVal != val)
					{
						if (text) target.attr('data-value', val);
						target.val(displayText); callHandlerV(target, val);
					}
				}
				target = $(menu).attr('data-icon');
				if (target)
				{
					target = $('#' + target);
					if (target.hasClass('item-icon'))
					{
						ReactDOM.render(React.createElement(Icon,{iconName:val}),target[0])
					}
				}
			}
		}

		var $parent = $this.parents('.open');
		clearMenus(e, $parent[0]);
		e.preventDefault(); e.stopPropagation();

		return true;
	}

	$(document)
		.on('click.px.dropdown', clearMenus)
		.on('click.px.dropdown', '.dropdown form', function (e) { e.stopPropagation() })
		.on('click.px.dropdown.data-api', toggle, Dropdown.prototype.toggle)
		.on('click.px.dropdown.data-api', '.dropdown-menu li', Dropdown.prototype.menuClick);

})(jQuery);
