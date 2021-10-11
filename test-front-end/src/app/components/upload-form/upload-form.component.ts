import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TransectionService } from 'src/app/services/transection.service';

@Component({
  selector: 'app-upload-form',
  templateUrl: './upload-form.component.html',
  styleUrls: ['./upload-form.component.scss']
})
export class UploadFormComponent implements OnInit {


  @ViewChild('fileInput') fileInput: ElementRef | undefined;
  file: any = {};
  errorMessage: string = '';
  valid: boolean = false;
  validExtension = ["xml", "csv"];

  constructor(private transectionService: TransectionService) { }

  ngOnInit(): void {
    this.file.name = 'Choose File';
  }

  browseFile(e: any): void {
    if (!e.target?.files[0]) {
      return;
    }
    this.errorMessage = '';
    this.valid = true;
    const file = e.target.files[0];
    console.log(file);
    this.file = file;
    if (this.file.size > 1000000) {
      this.errorMessage = "File 1 over 1 MB"
      this.valid = false;
      return;
    }

    const extension = this.file.name.split('.').pop()?.toLowerCase();
    if(!this.validExtension.includes(extension)){

      this.errorMessage = "Unknown Format";
      this.valid = false;
      return;
    }

  }


  async uploadFile(): Promise<void> {
    try {

      const formData = new FormData();
      formData.append('file', this.file, this.file.name);
      const result = await this.transectionService.upload(formData).toPromise();
      console.log(result);
      
    } catch (ex: any) {

      if (ex.status === 400) {
        this.errorMessage = ex.error;
        this.valid = false;
      }
      console.error(ex);

    }

  }

}
