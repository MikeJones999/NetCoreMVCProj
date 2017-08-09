// site.js

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