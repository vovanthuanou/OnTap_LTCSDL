import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  // lấy data về

  // public res: any;

  // constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //   http.post('https://localhost:44377/' + 'api/Products/get-all',null).subscribe(result => {
  //     this.res = result;
  //     console.log(this.res);
  //   }, error => console.log(error)
  //   )
  // }

  ngOnInit() {
  }

}
