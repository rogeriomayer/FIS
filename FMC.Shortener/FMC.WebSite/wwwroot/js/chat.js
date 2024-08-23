var TM2_URL = "https://fmc.tm2digital.com/chat";

var TM2_TOKEN = "dnbSa-hqpBq-622zY-CcsJW-JoahO";
var url = TM2_URL + '/api/webchat/' + TM2_TOKEN;
var xhr = new XMLHttpRequest();
xhr.open('GET', url);
xhr.onload = function () {
    if (xhr.status === 200) {
        var done = false;
        var script = document.createElement('script');
        script.async = true;
        script.type = 'application/javascript';
        script.src = TM2_URL + '/api/webchat/js/' + TM2_TOKEN;
        document.getElementById("chat_tm2").innerHTML = this.response;
        script.onreadystatechange = script.onload = function (e) {
            if (!done && (!this.readyState || this.readyState == 'loaded' || this.readyState == 'complete')) {
                done = true;
                startTm2Chat();
            }
        };
        document.getElementsByTagName('HEAD').item(0).appendChild(script);
    }
};
xhr.send(); 