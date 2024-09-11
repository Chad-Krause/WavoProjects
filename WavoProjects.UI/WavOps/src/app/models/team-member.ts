export class TeamMember {
  id: number = 0;
  name: string = "";
  trackTime: boolean = true;
  createdOn: Date = new Date();
  updatedOn: Date = new Date();
  deletedOn?: Date;

  constructor(obj: any) {
    Object.keys(obj).forEach(key => {
        this[key] = obj[key];
    });
  }
}
