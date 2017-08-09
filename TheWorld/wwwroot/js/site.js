// site.js


//wrapping in function hides the global naming issues - i.e all js are executed in global scope and therefore variables could override one another
(function () {

    //this script is referenced and called with in index.htm
    var ele = document.getElementById("username");
    ele.innerHTML = "Mikie Jones";


    //changing background colour of the main section of the index div 
    var main = document.getElementById("main");
    //assign annonymous function
    main.onmouseenter = function () {
        main.style.backgroundColor = "#888";
    };

    main.onmouseleave = function () {
        main.style.backgroundColor = "";
    };

})();
//last (); is actually calling this function itself you could always have parenthesis ("hello"); if required. 