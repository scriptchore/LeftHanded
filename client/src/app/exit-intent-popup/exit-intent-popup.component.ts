import { ChangeDetectorRef, Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-exit-intent-popup',
  templateUrl: './exit-intent-popup.component.html',
  styleUrls: ['./exit-intent-popup.component.scss']
})
export class ExitIntentPopupComponent implements OnInit{

  showPopup: boolean = false;
  discountPercentage: number = 1;
  interval: any;

  constructor(private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    console.log("Popup component loaded.");

    // 🔥 Check if the user already dismissed the popup
    if (localStorage.getItem("popupDismissed") === "true") {
      console.log("Popup was previously dismissed. Won't show again.");
      return; // Stop here if dismissed
    }
  }

  /** 🖥 Detects exit intent on desktop */
  @HostListener('document:mouseleave', ['$event'])
  onMouseLeave(event: MouseEvent) {
    if (event.clientY < 50 && !this.showPopup) { 
      console.log("Exit intent detected!");

      // ✅ If user previously dismissed, do not show the modal
      if (localStorage.getItem("popupDismissed") === "true") {
        console.log("User dismissed popup before. Won't show again.");
        return;
      }

      this.showPopup = true;
      this.startCountdown();

      setTimeout(() => {
        const modal = document.querySelector('.modal') as HTMLElement;
        if (modal) {
          modal.style.display = 'block';
          modal.style.opacity = '1';
          modal.style.transform = 'scale(1)';
        }
      }, 50);
    }
  }

  /**  Start countdown (1% to 10%) */
  startCountdown() {
    let count = 1;
    this.discountPercentage = count;
    this.cdr.detectChanges();

    this.interval = setInterval(() => {
      if (count >= 10) {
        clearInterval(this.interval);
        return;
      }
      count++;
      this.discountPercentage = count;
      this.cdr.detectChanges();
    }, 300);
  }

  /** ❌ Close popup & prevent future popups */
  closePopup() {
    console.log("Popup closed.");
    this.showPopup = false;
    clearInterval(this.interval);

    // 🔥 Save in localStorage to prevent it from showing again
    localStorage.setItem("popupDismissed", "true");

    setTimeout(() => {
      const modal = document.querySelector('.modal') as HTMLElement;
      if (modal) {
        modal.style.display = 'none';
      }
    }, 100);
  }
}
