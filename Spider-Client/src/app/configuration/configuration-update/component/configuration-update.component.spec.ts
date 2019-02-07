import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurationUpdateComponent } from './configuration-update.component';

describe('ApplicationAddComponent', () => {
  let component: ConfigurationUpdateComponent;
  let fixture: ComponentFixture<ConfigurationUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ConfigurationUpdateComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurationUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
