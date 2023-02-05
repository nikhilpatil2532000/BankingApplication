import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileNotFoundErrorComponent } from './file-not-found-error.component';

describe('FileNotFoundErrorComponent', () => {
  let component: FileNotFoundErrorComponent;
  let fixture: ComponentFixture<FileNotFoundErrorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileNotFoundErrorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FileNotFoundErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
