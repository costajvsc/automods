import { Component, Input, OnInit } from '@angular/core';
import { CarroService } from '../service/carro.service';
import { CategoriaService } from '../service/categoria.service';
import { MarcaService } from '../service/marca.service';

@Component({
  selector: 'app-carros-list',
  templateUrl: './carros-list.component.html',
  styleUrls: ['./carros-list.component.css']
})
export class CarrosListComponent implements OnInit {


  Carros: any = [];
  Categorias: any = [];
  Marcas: any = [];
  @Input() novoCarro = { modelo: '', potencia: 0, autonomia: 0, peso: 0, ano: 0, categoriaId: 0,  marcaId: 0}

  constructor(
    public carroService: CarroService,
    public categoriaService: CategoriaService,
    public marcaService: MarcaService
  ) { }

  ngOnInit(): void {
    this.LoadCarros();
    this.LoadCategorias();
    this.LoadMarcas();
  }

  LoadCarros(){
    return this.carroService.getCarros().subscribe((data: {}) => {
      this.Carros = data;
    })
  }

  LoadCategorias(){
    return this.categoriaService.getCategorias().subscribe((data: {}) => {
      this.Categorias = data;
    })
  }

  LoadMarcas(){
    return this.marcaService.getMarcas().subscribe((data: {}) => {
      this.Marcas = data;
    })
  }

  adicionarCarro(){
      this.carroService.createCarro(this.novoCarro).subscribe((data: {}) => {
        alert('Carro adicionado com sucesso');
      })
  }

  removerCarro(id : any){
    if(window.confirm('Tem certeza que deseja realizar esta operação?')){
      this.carroService.deleteCarro(id).subscribe((data: any) => {
        alert('Carro excluido com sucesso')
        this.LoadCarros()
      })
    }
  }

}
