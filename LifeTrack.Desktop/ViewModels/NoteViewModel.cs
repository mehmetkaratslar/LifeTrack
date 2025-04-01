using LifeTrack.Core;
using LifeTrack.Core.Models;
using LifeTrack.Desktop.Commands;
using LifeTrack.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LifeTrack.Desktop.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        private readonly NoteService _noteService;
        private ObservableCollection<Note> _notes;
        private Note _selectedNote;
        private Note _newNote;

        public NoteViewModel(NoteService noteService)
        {
            _noteService = noteService;
            NewNote = new Note { CreatedAt = DateTime.Now };

            LoadNotesCommand = new RelayCommand(async () => await LoadNotes());
            AddNoteCommand = new RelayCommand(async () => await AddNote());
            UpdateNoteCommand = new RelayCommand(async () => await UpdateNote(), () => SelectedNote != null);
            DeleteNoteCommand = new RelayCommand(async () => await DeleteNote(), () => SelectedNote != null);

            LoadNotes();
        }

        public NoteViewModel()
        {
        }

        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        public Note NewNote
        {
            get => _newNote;
            set
            {
                _newNote = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadNotesCommand { get; }
        public ICommand AddNoteCommand { get; }
        public ICommand UpdateNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }

        private async Task LoadNotes()
        {
            var notes = await _noteService.GetAllAsync();
            Notes = new ObservableCollection<Note>(notes);
        }

        private async Task AddNote()
        {
            if (string.IsNullOrWhiteSpace(NewNote.Title) || string.IsNullOrWhiteSpace(NewNote.Content))
                return;

            await _noteService.AddAsync(NewNote);
            NewNote = new Note { CreatedAt = DateTime.Now };
            await LoadNotes();
        }

        private async Task UpdateNote()
        {
            if (SelectedNote == null || string.IsNullOrWhiteSpace(SelectedNote.Title))
                return;

            SelectedNote.ModifiedAt = DateTime.Now;
            await _noteService.UpdateAsync(SelectedNote);
            await LoadNotes();
        }

        private async Task DeleteNote()
        {
            if (SelectedNote == null)
                return;

            await _noteService.DeleteAsync(SelectedNote.Id);
            await LoadNotes();
        }
    }
}