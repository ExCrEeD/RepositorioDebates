import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PoliStreamComponent } from './poli-stream.component';

describe('PoliStreamComponent', () => {
  let component: PoliStreamComponent;
  let fixture: ComponentFixture<PoliStreamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PoliStreamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PoliStreamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
