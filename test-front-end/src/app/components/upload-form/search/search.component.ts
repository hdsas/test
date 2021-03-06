import { Component, OnInit } from '@angular/core';
import { TransactionService } from 'src/app/services/transaction.service';
import * as moment from 'moment';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

  statusList = [
    { name: 'Approve', code: 'A' },
    { name: 'Rejected', code: 'R' },
    { name: 'Done', code: 'D' },
  ];

  result: any[] = [];

  constructor(
    private transactionService: TransactionService
  ) { }

  ngOnInit(): void {
  }

  async search(e: any): Promise<void> {
    console.log(e.form.value);


    const form: any = {
      id: e.form.value.id,
      statusCode: e.form.value.statusCode,
      startDate: e.form.value?.startDate ? moment(e.form.value?.startDate ).format("YYYY-MM-DD") : null,
      endDate: e.form.value?.endDate ? moment(e.form.value?.endDate ).format("YYYY-MM-DD") : null
    };

    this.result = await this.transactionService.search(form).toPromise();
  }

}
