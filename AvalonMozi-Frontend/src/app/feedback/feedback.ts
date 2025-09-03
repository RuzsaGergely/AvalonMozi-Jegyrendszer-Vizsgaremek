import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { Footer } from '../footer/footer';
import { FeedbackClient, FeedbackDto } from '../../services/moziHttpClient';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-feedback',
  imports: [
    LocalSharedModule,
    Header,
    Footer
  ],
  providers: [MessageService],
  templateUrl: './feedback.html',
  styleUrl: './feedback.css'
})
export class Feedback implements OnInit {

  public feedbackData: FeedbackDto = {
    message: "",
    email: "",
    name: "",
    phone: ""
  } as FeedbackDto;

  public verificationCode: number = 0;
  public showForm: boolean = true;
  public showSuccess: boolean = false;
  public showFailed: boolean = false;

  constructor(
    private feedbackClient: FeedbackClient,
    private messageService: MessageService
  ){}


  ngOnInit(): void {
    
  }

  sendFeedback() {
    if(
      this.feedbackData.message != "" &&
      this.feedbackData.email != "" &&
      this.feedbackData.name != ""
    ) {
      this.feedbackClient.newFeedback(this.feedbackData).subscribe(x=> {
        if(x.status == 200) {
          this.showForm = false;
          this.showSuccess = true;
          this.showFailed = false;
        } else {
          this.showForm = false;
          this.showSuccess = false;
          this.showFailed = true;
        }
      })
    }
  }

}
