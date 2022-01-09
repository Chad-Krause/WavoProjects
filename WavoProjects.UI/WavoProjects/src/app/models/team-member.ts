export class TeamMember {
  id: number;
  name: string;
  trackTime: boolean;
  createdOn: Date;
  updatedOn: Date;
  deletedOn: Date;

  constructor(obj: any) {
    Object.keys(obj).forEach(key => {
        this[key] = obj[key];
    });
  }
}
