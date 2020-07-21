import { AfterViewInit, Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, MatSortable } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Story } from '../data/story';
import { HackerNewsService } from '../data/hacker-news.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-hacker-news-table',
  templateUrl: './hacker-news-table.component.html',
  styleUrls: ['./hacker-news-table.component.css']
})
export class HackerNewsTableComponent implements AfterViewInit, OnInit, OnDestroy {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<Story>;
  dataSource: MatTableDataSource<Story>;
  dataSubscription: Subscription;
  loading: boolean = false;
  stories: Story[] = [];

  displayedColumns = ['title', 'by', 'time'];

  constructor(private hackerNewsService: HackerNewsService) {}

  ngOnInit() {
    //setting the stories. On first load this will take a few seconds, afterward data will cache on the backend and loads will be much quicker
    this.setStories(true);

  }

  ngAfterViewInit() {
  }


  ngOnDestroy(): void {
    //this.dataSubscription.unsubscribe();
  }

  //once data is retrieved, set up the table
  setStories(setTable: boolean) {
    this.loading = true;
    this.dataSubscription = this.hackerNewsService.getStories().subscribe(data => {
      this.stories = data;
      this.loading = false;

      //controls whether or not the datatable needs to be set up.
      if (setTable == true) {
        this.setUpDataTable();
      }
    })
  }

  setUpDataTable() {
    this.dataSource = new MatTableDataSource<Story>(this.stories);

    //since we're looking for the newest articles, assuming we want to see the newest first
    this.sort.sort({id: 'time', start: 'desc'} as MatSortable);
    this.dataSource.sort = this.sort;

    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  //when the row of the story is clicked, route to that url
  goToArticle(url: string) {
    window.location.href = url;
  }
}
