import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  public res: any;
  public listCategory = []; //tạo 1 mảng trống

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.post('https://localhost:44377/' + 'api/Categories/get-all',null).subscribe(result => {
      this.res = result; //lấy data về kt trong network f12
      //truyền data vào mảng mới tại
      this.listCategory = this.res.data;
      console.log(this.res);
      console.log(this.listCategory); //show trong console f12 xem data có được lưu vàp trong mảng mới tạo chưa
    }, error => console.error(error));
  }
 

  ngOnInit() {
  }

}
