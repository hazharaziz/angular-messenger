import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-general',
  templateUrl: './general.component.html',
  styleUrls: ['./general.component.css']
})
export class GeneralComponent implements OnInit {
  messageForm: FormControl;
  items: any[];

  constructor(private fb: FormBuilder) {
    this.messageForm = fb.control('', Validators.required);
    this.items = [
      {
        date: '02-02-2020',
        messages: [
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          },
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          },
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          },
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          },
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          }
        ]
      },
      {
        date: '03-02-2020',
        messages: [
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          },
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          },
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          },
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          },
          {
            text: 'hello',
            composer: 'hazhar',
            time: '13:34'
          }
        ]
      }
    ];
  }

  ngOnInit(): void {}

  onSendMessage(): void {
    if (this.messageForm.invalid) {
      return;
    }
    log(this.messageForm.value);
  }
}
