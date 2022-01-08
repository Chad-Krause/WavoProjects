import { Component, Input, OnInit } from '@angular/core';
import { TeamMemberTimesheetRow } from 'src/app/models/team-member-timesheet-row';

@Component({
  selector: 'team-member-timesheet-detail',
  templateUrl: './team-member-timesheet-detail.component.html',
  styleUrls: ['./team-member-timesheet-detail.component.scss']
})
export class TeamMemberTimesheetDetailComponent implements OnInit {
  @Input() teamMember: TeamMemberTimesheetRow;
  
  constructor() { }

  ngOnInit(): void {
  }

}
