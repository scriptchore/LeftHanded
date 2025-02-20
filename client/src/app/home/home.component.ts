import { AfterViewInit, Component, OnInit } from '@angular/core';



// Extend the window object to include the Typed.js property
declare global {
  interface Window {
    Typed: any;
  }
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements AfterViewInit {

 

  ngAfterViewInit(): void {
       // Load jQuery
       this.loadScript('https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js').then(() => {
        // Load Typed.js after jQuery is loaded
        this.loadScript('../../assets/js/typed.min.js').then(() => {
          // Initialize Typed.js
          this.initializeTypedJs();
        }).catch(error => console.error('Error loading Typed.js:', error));
      }).catch(error => console.error('Error loading jQuery:', error));
  }


  private loadScript(src: string): Promise<void> {
    return new Promise((resolve, reject) => {
      const script = document.createElement('script');
      script.src = src;
      script.type = 'text/javascript';
      script.async = true;
      script.onload = () => resolve();
      script.onerror = (error) => reject(error);
      document.getElementsByTagName('head')[0].appendChild(script);
    });
  }


  private initializeTypedJs(): void {
    new window['Typed']('.typing', {
      strings: [
        "be trendy?", "express yourself?", "make a statement?", "upgrade yourself?",
        "be ahead of your game?", "stand out?", "change the norm?", "just be you?",
        "beee hot!?", "play with colors?", "be attractive?", "be lefthanded?",
        "be cool?", "be that girl?", "be fulfilled?"
      ],
      typeSpeed: 80,
      backDelay: 2500,
      startDelay: 0,
      backSpeed: 0,
      loop: true,
    });
  }


 

    



  }


