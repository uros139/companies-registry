import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { MenuComponent } from './shared/components/menu/menu.component';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MenuComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  showMenu = false;

  constructor(private router: Router) {}

  ngOnInit() {
    this.updateMenuVisibility(this.router.url);

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateMenuVisibility(event.urlAfterRedirects);
      }
    });
  }

  private updateMenuVisibility(url: string) {
    const cleanUrl = url.split('?')[0].split('#')[0];
    const authRoutes = ['/login', '/register'];
    this.showMenu = !authRoutes.includes(cleanUrl);
  }
}
