import { Component, OnInit } from '@angular/core';
import { FooClient, Foo } from './foo.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app1';
  data: Foo[];

  constructor(private fooClient: FooClient) {

  }

  ngOnInit(): void {
    this.fooClient.getAll().subscribe(data => {
      this.data = data;
    });
  }
}
