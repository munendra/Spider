import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurationAddComponent } from './configuration-add.component';

describe('ApplicationAddComponent', () => {
  let component: ConfigurationAddComponent;
  let fixture: ComponentFixture<ConfigurationAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ConfigurationAddComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurationAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
