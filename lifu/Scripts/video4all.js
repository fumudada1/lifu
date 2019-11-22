(function() {

var videoElement = document.createElement('video');

window.__processVideo = function(videoElement) {
        var div = document.createElement('div');
        var sources = videoElement.getElementsByTagName('source');
        for (var i = 0; i < sources.length; i++) {
                if (sources[i].getAttribute('type') == 'video/mp4') {
                        src = sources[i].getAttribute('src');
                        break;
                }
        }

        var autoplay = new Boolean(videoElement.getAttribute('autoplay'));
        var autobuffer = new Boolean(videoElement.getAttribute('autobuffer'));

        var flashvars = "config={'playerId':'player','clip':{'autoPlay':" + autoplay + ", 'autoBuffering':" + autobuffer + ",'url':'" + src + "'},'playlist':[{'url':'" + src + "'}]}";
        html = '<object type="application/x-shockwave-flash" width="100%" height="100%">'
                + ' <param name="flashvars" value="' + flashvars + '" />'
                + ' <param name="movie" value="flowplayer-3.1.1.swf" />'
                + '</object>';
        div.innerHTML = html;
	videoElement.appendChild(div);

	div.style.cssText = videoElement.style.cssText;
}

// Test for native support first
if (("autoplay" in videoElement))
	return;

// No native support, let's figure out how to do it	

// IE-style behaviors?
if ("behavior" in videoElement.style) {
	var style = document.createElement('style');
	style.type = 'text/css';
	style.styleSheet.cssText = "video { behavior: url(video4all.htc); }";

	var anchorScript = document.getElementsByTagName('script')[0];
	anchorScript.parentElement.insertBefore(style, anchorScript);

	return;
}

// Mozilla-style bindings?
if ("MozBinding" in videoElement.style) {
        var style = document.createElement('style');
        style.type = 'text/css';
        style.textContent = 'video { display: block; -moz-binding: url("video4all.xml#video4all"); }';

	var anchorScript = document.getElementsByTagName('script')[0];
	anchorScript.parentNode.insertBefore(style, anchorScript);

	return;
}
	
window.setInterval(function() {
	var videos = document.body.getElementsByTagName('video');
	for (var i = 0; i < videos.length; i++) {
		if (videos[i].__processed)
			continue;

		videos[i].__processed = true;		
		window.__processVideo(videos[i]);
	}
}, 3000);

})();

