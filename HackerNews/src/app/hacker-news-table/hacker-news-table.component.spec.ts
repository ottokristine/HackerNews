import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HackerNewsTableComponent } from './hacker-news-table.component';
import { HackerNewsService } from '../data/hacker-news.service';
import { of } from 'rxjs';

describe('HackerNewsTableComponent', () => {
  let fixture: ComponentFixture<HackerNewsTableComponent>;
  let mockHackerNewsService;
  var fakeStoriesArray;

  beforeEach(() => {

    //set up a mock service for the get
    fakeStoriesArray = [{
      by:"open-source-ux",
      descendants:0,
      id:23892740,
      score:1,
      time:1595195401,
      title:"Architect Alfred Waterhouse and His Iconic Natural History Museum Building",
      type:"story",
      url:"https://www.nhm.ac.uk/discover/alfred-waterhouse-museum-building-cathedral-to-nature.html"
    },
    {
      by:"aarmaintenance",
      descendants:0,
      id: 23892719,
      score:1,
      time:1595195174,
      title:"How to Choose Between Metallic and Non-Metallic Gaskets",
      type:"story",
      url:"https://instantadhesivesdubai.wordpress.com/2020/07/19/how-to-choose-between-metallic-and-non-metallic-gaskets/"
    }]

    mockHackerNewsService = jasmine.createSpyObj(['getStories']);

    TestBed.configureTestingModule({
      declarations: [ HackerNewsTableComponent ],
      providers: [{provide: HackerNewsService, useValue: mockHackerNewsService}]
    }).compileComponents();

    fixture = TestBed.createComponent(HackerNewsTableComponent);
  });


  it('Should return Stories when dataService is called', () => {
    mockHackerNewsService.getStories.and.returnValue(of(fakeStoriesArray))
    fixture.componentInstance.setStories(false);


    expect(fixture.componentInstance.stories.length).toBe(2);

  });

  it('should assign correct values to array', () => {
    mockHackerNewsService.getStories.and.returnValue(of(fakeStoriesArray))
    fixture.componentInstance.setStories(false);

    expect(fixture.componentInstance.stories[0].id).toBe(23892740);
  })

  });
