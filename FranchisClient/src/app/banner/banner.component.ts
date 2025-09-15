import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-banner',
  standalone: true,
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.css'],
  imports: [CommonModule]
})
export class BannerComponent {
  @Input() images: { url: string; caption?: string }[] = [];
  currentIndex = 0;
  intervalId: any;

  ngOnInit() {
          console.log('Banner images:', this.images);
    this.startRotation();
  }

  ngOnDestroy() {
    clearInterval(this.intervalId);
  }

  startRotation() {
    this.intervalId = setInterval(() => {
      this.currentIndex = (this.currentIndex + 1) % this.images.length;
    }, 3500);
  }

  goTo(index: number) {
    this.currentIndex = index;
  }
}
