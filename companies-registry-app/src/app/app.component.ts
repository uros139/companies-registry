import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { MenuComponent } from './shared/components/menu/menu.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MenuComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'companies-registry-app';
  showMenu = false;

  constructor(private router: Router) { }

  ngOnInit() {
    // Check initial route
    this.updateMenuVisibility(this.router.url);

    // Listen to route changes
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.updateMenuVisibility(event.url);
      }
    });
  }
  private updateMenuVisibility(url: string) {
    const authRoutes = ['/login', '/register'];
    this.showMenu = !authRoutes.some(route => url.includes(route));
  }
}