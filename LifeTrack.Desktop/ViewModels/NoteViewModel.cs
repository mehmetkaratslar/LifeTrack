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
        private string _searchText;


        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                // Arama metni değiştiğinde notları filtrelemek için
                FilterNotes();
            }
        }

        private void FilterNotes()
        {
            // Burada SearchText'e göre notları filtreleyebilirsiniz
            // Örnek:
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // Arama kutusu boşsa tüm notları göster
                // Eğer bir ObservableCollection<Note> kullanıyorsanız:
                // FilteredNotes = new ObservableCollection<Note>(AllNotes);
            }
            else
            {
                // Arama kriterine göre filtrele
                // FilteredNotes = new ObservableCollection<Note>(
                //     AllNotes.Where(n => n.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                //                      n.Content.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                // );
            }
        }
        public NoteViewModel(NoteService noteService)
        {
            _noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
            NewNote = new Note { CreatedAt = DateTime.Now };

            LoadNotesCommand = new RelayCommand(async () => await LoadNotes());
            AddNoteCommand = new RelayCommand(async () => await AddNote());
            UpdateNoteCommand = new RelayCommand(async () => await UpdateNote(), () => SelectedNote != null);
            DeleteNoteCommand = new RelayCommand(async () => await DeleteNote(), () => SelectedNote != null);
        }

        public void Initialize()
        {
            // Başlangıçta verileri yükle
            LoadNotes();
        }

        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set => SetProperty(ref _selectedNote, value);
        }

        public Note NewNote
        {
            get => _newNote;
            set => SetProperty(ref _newNote, value);
        }

        public ICommand LoadNotesCommand { get; }
        public ICommand AddNoteCommand { get; }
        public ICommand UpdateNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }

        private async Task LoadNotes()
        {
            try
            {
                var notes = await _noteService.GetAllAsync();
                Notes = new ObservableCollection<Note>(notes);
            }
            catch (Exception ex)
            {
                // Hata işleme burada yapılabilir
                Console.WriteLine($"Notları yüklerken hata: {ex.Message}");
            }
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