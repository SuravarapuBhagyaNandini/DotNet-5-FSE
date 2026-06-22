// exercises.js — jQuery Exercises implementations
// Exercise 1 bonus: log Hello World using jQuery when ready
$(document).ready(function(){
  console.log('Hello World! (from jQuery)');

  // Exercise 2: change h1 text and hide one p on button click
  $('h1').text('jQuery Exercises — DOM Ready');
  $('#btnHideP').on('click', function(){ $('#p2').hide(); });

  // Exercise 3: box methods
  $('#hideBoxes').on('click', function(){ $('.box').hide(); });
  $('#showBoxes').on('click', function(){ $('.box').show(); });
  $('#fadeOut').on('click', function(){ $('.box').fadeOut(); });
  $('#fadeIn').on('click', function(){ $('.box').fadeIn(); });
  $('#toggleBoxes').on('click', function(){ $('.box').toggle(); });
  $('#chain').on('click', function(){ $('.box').slideUp().delay(1000).slideDown(); });

  // Exercise 4: form add li and remove all
  $('#addItem').on('click', function(){
    var val = $('#itemInput').val();
    if (!val) return;
    var li = $('<li>').text(val);
    $('#items').append(li);
    $('#itemInput').val('');
  });
  $('#removeAll').on('click', function(){ $('#items').empty(); });

  // Exercise 5: color box click/dblclick
  $('#colorBtn').on('click', function(){ $('#colorBox').css('background','red'); });
  $('#colorBox').on('dblclick', function(){ $(this).css('background','white'); });

  // Exercise 6: mouse and keyboard events
  $('#mouseBox').on('click', function(){ $(this).css('background','#ffe0b2'); });
  $('#mouseBox').on('dblclick', function(){ $(this).css('background','#c8e6c9'); });
  $('#mouseBox').on('mouseenter', function(){ $(this).append('<div class="small">Mouse in</div>'); });
  $('#mouseBox').on('mouseleave', function(){ $(this).find('.small').remove(); });

  $('#keyInput').on('keypress', function(e){
    var ch = String.fromCharCode(e.which || e.keyCode);
    console.log('Key pressed:', ch);
  });
});
