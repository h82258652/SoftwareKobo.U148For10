"use strict";

function initImgs() {
    var imgs = document.getElementsByTagName("img");
    var i, img, parentNode, tagName;
    for (i = 0; i < imgs.length; i += 1) {
        img = imgs[i];

        img.removeAttribute("class");
        img.removeAttribute("style");
        img.removeAttribute("width");
        img.removeAttribute("height");
        img.style.display = "block";
        img.onerror = function () {
            this.style.display = "none";
        };

        parentNode = img.parentNode;
        if (parentNode) {
            tagName = parentNode.tagName;
            if (tagName === "A" || tagName === "a") {
                parentNode.setAttribute("href", "javascript:void(0);");
            }
        }
    }
}

function setThemeMode(theme) {
    if (theme.toLowerCase() === "dark") {
        document.getElementsByTagName("html")[0].setAttribute("class","night");
    }
    else {
        document.getElementsByTagName("html")[0].setAttribute("class", "");
    }
}

function setContent(content) {
    document.getElementById("content").innerHTML = content;
    initImgs();
}

function setTitle(title) {
    document.getElementById("title").innerHTML = title;
}

function setLeft(left) {
    document.getElementById("left").innerHTML = left;
}

function setRight(right) {
    document.getElementById("right").innerHTML = right;
}