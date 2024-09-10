export class TimesheetTeamMember {
    id: number;
    name: string;
    clockedIn: boolean;

    constructor(obj: any) {
        if(obj) {
            Object.keys(obj).forEach(key => {
                this[key] = obj[key];
            });
        }
    }
}