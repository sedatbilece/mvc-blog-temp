;(function($){

  $(document).ready(function(){
    
          // counter area
          const ucounter = $('.counter');
     
          if (ucounter.length > 0) {
           ucounter.countUp();  
          }


          $(".video-play-button").modalVideo();

           // on page load...
    moveProgressBar();
    // on browser resize...
    $(window).resize(function() {
        moveProgressBar();
    });




    // SIGNATURE PROGRESS
    function moveProgressBar() {
      console.log("moveProgressBar");
        var getPercent = ($('.progress-wrap3').data('progress-percent') / 100);
        var getProgressWrapWidth = $('.progress-wrap3').width();
        var progressTotal = getPercent * getProgressWrapWidth;
        var animationLength = 2500;
        
        // on page load, animate percentage bar to data percentage length
        // .stop() used to prevent animation queueing
        $('.progress-bar3').stop().animate({
            left: progressTotal
        }, animationLength);
    }


          //  nice select
          $('select').niceSelect();

          $('.tab-btn-wrapper button').click(function(){
            $('button').removeClass("active-item");
            $(this).addClass("active-item");
        });




          /*---------------------------------------------------
            Tooltip
        ----------------------------------------------------*/

        tippy('#tippy_bar', {
          content: "Marketing & Consultation",
          placement: 'top-end',
          animation: 'shift-toward-extreme',
      });



      $("#cta-input").click(function(){
          $("#show-hide-submenu").toggleClass('show-input');
      });




      /*---------------------------------------------------
          Isotop filter course 
      ----------------------------------------------------*/

  
      // init Isotope
      var $grid = $('.port-filter').isotope({
          // options
      });
      
      // filter items on button click
      $('.tab-btn-wrapper').on( 'click', 'button', function() {
          var filterValue = $(this).attr('data-filter');
          $grid.isotope({ filter: filterValue });
      });
    
 


// pricing-plan-tab
$("#ce-toggle").click(function (event) {
  $(".plan-toggle-wrap").toggleClass("active");
});

$("#ce-toggle").change(function () {
  if ($(this).is(":checked")) {
    $(".tab-content #monthly").hide();
    $(".tab-content #yearly").show();
  } else {
    $(".tab-content #monthly").show();
    $(".tab-content #yearly").hide();
  }
});

// color-selactor
const color = $(".contact-select li ");

color.on("click", function () {
  $(".contact-select li").removeClass("active-size");
  $(this).addClass("active-size");
});

// testimonial
$('.testimonial-carousel-area').owlCarousel({
  loop:true,
  margin:30,
  nav:false,
  dots:true,
  items: 10,
  smartSpeed:1300,
  autoplay:true,
  autoplayTimeout:3000,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:false,
       
          
      },
      600:{
          items:2,
      },
      1000:{
          items:2,
      }
  }
});


// testimonial
$('.testomonial2-carousel-area1').owlCarousel({
  loop:true,
  margin:30,
  nav:false,
  dots:true,
  items: 10,
  smartSpeed:1300,
  autoplay:true,
  autoplayTimeout:3000,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:false,
       
          
      },
      600:{
          items:2,
      },
      1000:{
          items:2,
      }
  }
});





// testimonial
$('.testomonial2-carousel-area').owlCarousel({
  loop:true,
  margin:30,
  nav:false,
  dots:true,
  items: 10,
  smartSpeed:1300,
  autoplay:true,
  autoplayTimeout:3000,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:false,
       
          
      },
      600:{
          items:2,
      },
      1000:{
          items:3,
          dots:true,
      }
  }
});





// testimonial
$('.cta4-carousel').owlCarousel({
  loop:true,
  margin:30,
  nav:false,
  dots:true,
  items: 10,
  smartSpeed:1300,
  autoplay:true,
  autoplayTimeout:3000,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:false,
       
          
      },
      600:{
          items:1,
      },
      1000:{
          items:1,
      }
  }
});


$('.testimonials9-carousel-area').owlCarousel({
  loop:true,
  margin:30,
  nav:false,
  dots:true,
  items:10,
  autoplay:true,
  smartSpeed:1300,
  autoplayTimeout:3000,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:false,
       
          
      },
      600:{
          items:2,
      },
      1000:{
          items:3,
      }
  }
});

$('.testimonials11-owl-carousel').owlCarousel({
  loop:true,
  margin:30,
  nav:false,
  dots:true,
  items:10,
  autoplay:true,
  smartSpeed:1300,
  autoplayTimeout:3000,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:false,
       
          
      },
      600:{
          items:2,
      },
      1000:{
          items:3,
      }
  }
});


// testimonial
$('.testimonial5-section5-area').owlCarousel({
  loop:true,
  margin:30,
  nav:true,
  dots:false,
  navText:[" <i class='fa-solid fa-arrow-left-long'></i>","<i class='fa-solid fa-arrow-right-long'></i>"],
  smartSpeed:1300,
  items: 10,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:true,
       
          
      },
      600:{
          items:1,
      },
      1000:{
          items:1,
      }
  }
});


// testimonial
$('.testimonial10-owlcarousel').owlCarousel({
  loop:true,
  margin:30,
  nav:true,
  dots:false,
  navText:[" <i class='fa-solid fa-arrow-left-long'></i>","<i class='fa-solid fa-arrow-right-long'></i>"],
  items: 10,
  smartSpeed:1300,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:true,
       
          
      },
      600:{
          items:2,
      },
      1000:{
          items:3,
      }
  }
});


// testimonial
$('.testimonial7-carousel-area').owlCarousel({
  loop:true,
  margin:30,
  nav:true,
  dots:false,
  navText:[" <i class='fa-solid fa-arrow-left'></i>","<i class='fa-solid fa-arrow-right'></i>"],
  items: 10,
  smartSpeed:1300,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:true,
       
          
      },
      600:{
          items:2,
      },
      1000:{
          items:2,
      }
  }
});




// testimonial
$('.testimonial8-carousel-area').owlCarousel({
  loop:true,
  margin:30,
  nav:true,
  dots:false,
  navText:[ "<i class='fa-solid fa-arrow-right'></i>"," <i class='fa-solid fa-arrow-left'></i>"],
  items: 10,
  smartSpeed:1300,
  responsiveClass:true,
  responsive:{
      0:{
          items:1,
          nav:true,
       
          
      },
      600:{
          items:2,
      },
      1000:{
          items:2,
      }
  }
});




 // $( window ).scroll(function() {   
               // if($( window ).scrollTop() > 10){  // scroll down abit and get the action   
               $(".progress-bar").each(function(){
                each_bar_width = $(this).attr('aria-valuenow');
                $(this).width(each_bar_width + '%');
              });


   // sticky header active
    if ($("#header").length > 0) {
      $(window).on("scroll", function (event) {
        var scroll = $(window).scrollTop();
        if (scroll < 1) {
          $(".header-area").removeClass("sticky");
        } else {
          $(".header-area").addClass("sticky");
        }
      });
    }




   // page-progress
    var progressPath = document.querySelector(".progress-wrap path");
    var pathLength = progressPath.getTotalLength();
    progressPath.style.transition = progressPath.style.WebkitTransition =
      "none";
    progressPath.style.strokeDasharray = pathLength + " " + pathLength;
    progressPath.style.strokeDashoffset = pathLength;
    progressPath.getBoundingClientRect();
    progressPath.style.transition = progressPath.style.WebkitTransition =
      "stroke-dashoffset 10ms linear";
    var updateProgress = function () {
      var scroll = $(window).scrollTop();
      var height = $(document).height() - $(window).height();
      var progress = pathLength - (scroll * pathLength) / height;
      progressPath.style.strokeDashoffset = progress;
    };
      updateProgress();

    $(window).scroll(updateProgress);
    var offset = 50;
    var duration = 550;
    jQuery(window).on("scroll", function () {
      if (jQuery(this).scrollTop() > offset) {
        jQuery(".progress-wrap").addClass("active-progress");
      } else {
        jQuery(".progress-wrap").removeClass("active-progress");
      }
    });
    jQuery(".progress-wrap").on("click", function (event) {
      event.preventDefault();
      jQuery("html, body").animate({ scrollTop: 0 }, duration);
      return false;
    });

  });

 

   
  $(window).on('load', function(event) {

       setTimeout(function () {
            $("#pre-load").fadeToggle();
          }, 500);    
          
      //  aos
      AOS.init();

        });

  // progress bar
  $('#progressbar1').LineProgressbar();

   // progress bar
    $('#progressbar2').LineProgressbar();

  })(jQuery);
