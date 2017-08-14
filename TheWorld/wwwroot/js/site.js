// site.js


//wrapping in function hides the global naming issues - i.e all js are executed in global scope and therefore variables could override one another
(function () {

    //without JQuery
       // //this script is referenced and called with in index.htm
        //var ele = document.getElementById("username");
        //ele.innerHTML = "Mikie Jones";

       // //changing background colour of the main section of the index div 
        //var main = document.getElementById("main");

    //with JQuery - $ sign used to get the element - # to highlight the variable required

    //var ele = $("#username");
    //ele.text("Mike Jonesaaaa");

    //var main = $("#main");

    ////assign annonymous function
    ////main.on("mouseenter",function () {
    ////    main.style = "background-color : #888;";
    ////});

    //$("#main").mouseenter(function () {
    //    $("#main").css("background-color", "#888");
    //});


    //$("#main").mouseleave(function () {
    //    $("#main").css("background-color", "");
    //});

    ////gets list of menu items - class marked menu
    //var menuItems = $("ul.menu li a");
    //menuItems.on("click", function ()
    //{
    //    var me = $(this);
    //    alert(me.text());
    //});

    //get both items as a wrapped jquery object
    var $sidebarAndWrapper = $("#sidebar, #wrapper");

    //get button - then toggle it on and off
    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar"))
        {
            $(this).text("Show Sidebar");
        }
        else
        {
            $(this).text("Hide Sidebar");
        }
    });

})();
//last (); is actually calling this function itself you could always have parenthesis ("hello"); if required.



