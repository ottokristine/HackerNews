import { TestBed } from '@angular/core/testing';

import { HackerNewsService } from './hacker-news.service';
import { HttpClient, HttpHandler, HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Story } from './story';

describe('HackerNewsService', () => {
  let service: HackerNewsService;
  let httpModule: HttpClient;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [HackerNewsService]
    });
    service = TestBed.get(HackerNewsService);
  });


  //make sure that our call is returning
  it('should return values when called', () => {
    var newStories: Story[] = [];
    //before data is added length should be 0
    expect(newStories.length).toBeLessThanOrEqual(0)

    service.getStories().subscribe(data => {
      newStories = data;
      expect(newStories.length).toBeGreaterThan(0);
    })
  });
});
