import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';
import { AppState } from 'src/app/store';
import { RelationActions } from 'src/app/store/actions/relation.actinos';
import { SearchActions } from 'src/app/store/actions/search.actions';
import { SearchSelectors } from 'src/app/store/selectors/search.selectors';
import { log } from 'src/app/utils/logger';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  searchedUsers$: Observable<User[]> = this.store.select(SearchSelectors.selectSearchedUsers);
  searchForm: FormControl;

  constructor(private fb: FormBuilder, private store: Store<AppState>) {
    this.searchForm = this.fb.control('', Validators.required);
  }

  ngOnInit(): void {
    this.searchQuery('');
    this.searchForm.valueChanges.subscribe((query) => {
      this.searchQuery(query);
    });
  }

  onSubmitMessage(): void {
    if (this.searchForm.invalid) return;
    this.searchQuery(this.searchForm.value);
  }

  searchQuery(query: string): void {
    this.store.dispatch(SearchActions.SearchRequest({ query }));
  }

  followUser(userId: number) {
    this.store.dispatch(RelationActions.FollowRequest({ userId }));
    this.store.dispatch(SearchActions.RemoveSearchItem({ id: userId }));
  }
}
