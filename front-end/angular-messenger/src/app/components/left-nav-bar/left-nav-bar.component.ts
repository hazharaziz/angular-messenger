import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-left-nav-bar',
  templateUrl: './left-nav-bar.component.html',
  styleUrls: ['./left-nav-bar.component.css']
})
export class LeftNavBarComponent implements OnInit {
  @Input() page: 'general' | 'search' | 'directs' | 'groups' | 'profile';

  constructor() {}

  ngOnInit(): void {}
}
