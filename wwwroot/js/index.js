
  window.addEventListener("scroll", function(){
		var deg = document.querySelector("header.header");
          
            deg.classList.toggle('sticky', window.scrollY > 35);
      
        });
		
		
