$(function() {
  "use strict";

  var scroll = $(".scroll");
  var htmBod = $("html, body");
  var testimonials = $(".testimonials-carousel");
  var navbar = $(".navbar-default");
  var nvaLi = $(".navbar-default .navbar-nav > li > a, .dropdown-menu li a");

  $(window).on("scroll", function() {
    const scrollTop = $(this).scrollTop();
    // Show Scroll
    if (scrollTop > 600) {
      scroll.fadeIn();
    } else {
      scroll.fadeOut();
    }
    // Add Class To Navbar
    if (scrollTop > navbar.height()) {
      navbar.addClass("sticky-nav gradient-opacity");
    } else {
      navbar.removeClass("sticky-nav gradient-opacity");
    }
  });

  // Scroll To Top

  scroll.on("click", function() {
    htmBod.animate(
      {
        scrollTop: 0
      },
      1500,
      "easeInOutExpo"
    );
  });

  // Testimonials

  testimonials.owlCarousel({
    items: 1,
    dots: false,
    loop: true,
    rtl: true,
    nav: true,
    dots: true,
    autoplay: true,
    navText: ["<i class='fa fa-angle-left'>", "<i class='fa fa-angle-right'>"]
  });

  // Smooth Scroll

  nvaLi.on("click", function(e) {
    if (
      $(this)
        .attr("href")
        .startsWith("#")
    ) {
      htmBod.animate(
        {
          scrollTop: $($(this).attr("href")).offset().top
        },
        1500,
        "easeInOutExpo"
      );
      e.preventDefault();
    }
  });

  $(document).ready(function() {
    AOS.init();
  });
});
