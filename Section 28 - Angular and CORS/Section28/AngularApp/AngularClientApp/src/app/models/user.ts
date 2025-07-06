export class User {
  recordID: string;
  name: string;
  email: string;
  country: string;
  createdOn: string;
  modifiedOn: string;

  constructor(RecordID: string, Name: string, Email: string, Country: string, CreatedOn: string, ModifiedOn: string) {
    this.recordID = RecordID;
    this.name = Name;
    this.email = Email;
    this.country = Country;
    this.createdOn = CreatedOn;
    this.modifiedOn = ModifiedOn;
  }
}
