import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  standalone: true,
  imports: [],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css'
})
export class ServerErrorComponent {

  error: any;

  // Navagation extras can only be accessed from constructor
  constructor(private router: Router) {
    
    const navigationX = router.getCurrentNavigation();
    this.error = navigationX?.extras?.state?.['error1'];
    
  }
}
