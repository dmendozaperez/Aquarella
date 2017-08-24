// Require EasyJQuery After JQuery

$(document).ready(function () {
    $("input[id$='UserName']").focus();
    // 2. Setup Callback Function
    // EasyjQuery_Get_IP("my_callback"); // fastest version

    try {
        EasyjQuery_Get_IP("my_callback2", "full"); // full version
    }
    catch (err) {
        //Handle errors here
    }
    /*Other option*/
    /*   $.getJSON("http://jsonip.appspot.com?callback=?",
    function (data) {
    alert("Your ip: " + data.ip + " Hostname:" + window.location.hostname);
    });*/
});

// 1. Your Data Here
function my_callback(json) {
    alert("IP :" + json.IP + " nCOUNTRY: " + json.COUNTRY);
}

function my_callback2(json) {
    // more information at http://api.easyjquery.com/test/demo-ip.php
    var urlMethod = "LoginForm.aspx/setUserInfo";    
    var jsonData = '{userIp:"' + json.IP + '", userCountry:"' + json.COUNTRY + '", userRegion:"' + json.regionName + '", userCity:"' + json.cityName + '", userName:""}';
    SendAjax(urlMethod, jsonData);
}

//Ajax
function SendAjax(urlMethod, jsonData) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: urlMethod,
        data: jsonData,
        dataType: "json",
        //async: true,
        success: function (msg) {
        }
    });
}