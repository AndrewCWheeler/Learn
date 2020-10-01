// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
  $(document).scroll(function () {
    var $nav = $('.navbar.fixed-top');
    $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
  });
  $('.flashCard').click(function flip() {
    $(this).toggleClass('flipped');
  });
  $('#signUpModal').on('shown.bs.modal', function () {
    $('#modalButton').trigger('focus');
  });
  $('.fraction').each(function (key, value) {
    $this = $(this);
    var split = $this.html().split('/');
    if (split.length == 2) {
      $this.html(
        '<span class="top">' +
          split[0] +
          '</span><span class="bottom">' +
          split[1] +
          '</span>'
      );
    }
  });
});
