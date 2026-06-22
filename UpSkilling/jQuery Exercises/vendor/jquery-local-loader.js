/* jquery-local-loader.js
   Simulates a locally-included jQuery file by dynamically loading the official CDN
   copy and then loading exercises.js. This approach demonstrates a local script
   tag that bootstraps jQuery when an actual local copy isn't bundled.
*/
(function(){
  var s = document.createElement('script');
  s.src = 'https://code.jquery.com/jquery-3.6.0.min.js';
  s.onload = function(){
    console.log('jQuery loaded via local loader (CDN fallback)');
    var ex = document.createElement('script');
    ex.src = 'exercises.js';
    document.head.appendChild(ex);
  };
  s.onerror = function(){ console.error('Failed to load jQuery from CDN.'); };
  document.head.appendChild(s);
})();
