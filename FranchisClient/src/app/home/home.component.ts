import { Component } from '@angular/core';
import { BannerComponent } from '../banner/banner.component';
import { ProductGridComponent } from '../products/product-grid.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BannerComponent, ProductGridComponent],
  templateUrl: './home.component.html'
})
export class HomeComponent {
  public bannerImages = [
    {
      url: 'assets/ipl1.png',
      caption: 'Welcome to the Indian Premier League!'
    },
    {
      url: 'assets/ipl2.png',
      caption: 'Cheer for your favorite IPL team!'
    },
    {
      url: 'assets/ipl3.png',
      caption: 'Shop Official IPL Merchandise'
    }
  ];
}
