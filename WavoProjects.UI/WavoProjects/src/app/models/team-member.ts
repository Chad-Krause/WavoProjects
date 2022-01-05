export class TeamMember {
  id: number;
  name: string;
  trackTime: boolean;

  constructor(obj: any) {
    Object.keys(obj).forEach(key => {
        this[key] = obj[key];
    });
  }
}
